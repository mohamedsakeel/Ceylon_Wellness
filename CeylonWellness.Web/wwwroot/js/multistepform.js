document.addEventListener('DOMContentLoaded', function () {
    // Function to load a step
    function loadStep(partialName, pushState = true) {
        $.get('/Form/LoadPartial?partialName=' + partialName, function (partialHtml) {
            $("#form-container").html(partialHtml);
            if (pushState) {
                // Push the new state into the history stack
                window.history.pushState({ partialName: partialName }, null, `?step=${partialName}`);
            }
        });
    }

    // Handle back/forward navigation
    window.addEventListener('popstate', function (event) {
        if (event.state && event.state.partialName) {
            loadStep(event.state.partialName, false); // Do not push state when handling popstate
        } else {
            loadStep('Steps/_SelectAge', false); // Default to the first step if no state is available
        }
    });

    // Initial load
    let initialStep = new URLSearchParams(window.location.search).get('step') || 'Steps/_SelectAge';
    loadStep(initialStep, false); // Do not push state for the initial load

    // Bind the loadNextStep function to form submissions or other triggers
    document.getElementById('form-container').addEventListener('submit', function (e) {
        e.preventDefault();
        var partialName = $(e.target).data('next-step');
        loadNextStep(partialName);
    });
});
function loadNextStep(partialName) {
    // Serialize form data from the current step
    var data = $('#multistepform').serialize();

    $.ajax({
        type: "POST",
        url: '/Form/SaveStepData',
        data: data,
        success: function () {
            // Load the next partial view
            $.get('/Form/LoadPartial?partialName=' + partialName, function (partialHtml) {
                // Replace the content inside the form container with the loaded partial view
                $("#form-container").html(partialHtml);
                // Push the new state into the history stack
                window.history.pushState({ partialName: partialName }, null, `?step=${partialName}`);
            });
        },
        error: function () {
            // Handle potential errors here (e.g., display an error message)
            console.error("Error saving or loading partial view.");
        }
    });
}

function ageVerification(partialName) {
    const ageInput = document.getElementById("Age");
    const age = ageInput.value;

    // Age Validation
    if (age.trim() === "" || age <= 0) {
        // Create a Bootstrap tooltip
        let tooltip = new mdb.Tooltip(ageInput, {
            title: "Please enter a valid age",
            placement: 'bottom'
        });
        tooltip.show();

        // Optionally clear input and hide tooltip after a delay
        setTimeout(() => {
            ageInput.value = '';
            tooltip.hide();
        }, 2000); // Hide tooltip after 2 seconds

        return; // Don't continue if invalid
    }

    loadNextStep(partialName);
}

function setGender(value) {
    document.getElementById("gender").value = value;
    var partialName = "Steps/_SetAge";
    loadNextStep(partialName);
}

function WHVerification(partialName) {
    const weightInput = document.getElementById("weight");
    const heightInput = document.getElementById("height");

    const weight = weightInput.value;
    const height = heightInput.value;

    let bmis = calculateBMI(weight, height);

    document.getElementById('bmi').value = bmis;

    if (weight.trim() === "" || weight <= 0) {
        // Create a Bootstrap tooltip for weight input
        let weightTooltip = new mdb.Tooltip(weightInput, {
            title: "Please enter a valid weight",
            placement: 'bottom'
        });
        weightTooltip.show();

        // Optionally clear weight input and hide tooltip after a delay
        setTimeout(() => {
            //weightInput.value = '';
            weightTooltip.hide();
        }, 2000); // Hide tooltip after 2 seconds

        return; // Don't continue if weight is invalid
    }

    if (height.trim() === "" || height <= 0) {
        // Create a Bootstrap tooltip for height input
        let heightTooltip = new mdb.Tooltip(heightInput, {
            title: "Please enter a valid height",
            placement: 'bottom'
        });
        heightTooltip.show();

        // Optionally clear height input and hide tooltip after a delay
        setTimeout(() => {
            //heightInput.value = '';
            heightTooltip.hide();
        }, 2000); // Hide tooltip after 2 seconds

        return; // Don't continue if height is invalid
    }

    document.getElementById('weightss').value = weight;

    // If both weight and height are valid, proceed to the next step
    loadNextStep(partialName);
}

//BMI Calculator
function calculateBMI(weight, height) {
    if (height <= 0 || weight <= 0) {
        return "Height and weight must be greater than zero";
    }
    let heightInMeters = height / 100;
    let bmi = weight / (heightInMeters * heightInMeters);
    return bmi.toFixed(2);
}

//target weight verification goal page
function TargetWeightVerification(partialName) {
    const targetWeightInput = document.getElementById('targetweight');
    const targetWeightValue = targetWeightInput.value;

    const weight = document.getElementById('weightss').value;
    const goalinput = document.getElementById('goal');
    const goal = document.getElementById('goal').value;

    if (goal == "") {
        let goalTooltip = new mdb.Tooltip(goalinput, {
            title: "Please select a goal",
            placement: 'bottom'
        });
        goalTooltip.show();

        // Optionally clear height input and hide tooltip after a delay
        setTimeout(() => {
            //targetWeightInput.value = '';
            heightTooltip.hide();
        }, 2000); // Hide tooltip after 2 seconds
        return false;
    }
    if (goal == "Gain Weight") {
        // Validate target weight input
        if (targetWeightValue < weight) {
            let heightTooltip = new mdb.Tooltip(targetWeightInput, {
                title: "Please enter a Target weight more than your weight",
                placement: 'bottom'
            });
            heightTooltip.show();

            // Optionally clear height input and hide tooltip after a delay
            setTimeout(() => {
                //targetWeightInput.value = '';
                heightTooltip.hide();
            }, 2000); // Hide tooltip after 2 seconds
            return false;
        }
    }
    else if (goal == "Lose Weight") { 
        // Validate target weight input
        if (targetWeightValue > weight) {
            let heightTooltip = new mdb.Tooltip(targetWeightInput, {
                title: "Target weight cannot be more than your weight",
                placement: 'bottom'
            });
            heightTooltip.show();

            // Optionally clear height input and hide tooltip after a delay
            setTimeout(() => {
                //targetWeightInput.value = '';
                heightTooltip.hide();
            }, 2000); // Hide tooltip after 2 seconds
            return false;
        }
    }

    if (targetWeightValue.trim() === "" || targetWeightValue <= 0) {
        let heightTooltip = new mdb.Tooltip(targetWeightInput, {
            title: "Please enter valid target weight",
            placement: 'bottom'
        });
        heightTooltip.show();

        // Optionally clear height input and hide tooltip after a delay
        setTimeout(() => {
            //targetWeightInput.value = '';
            heightTooltip.hide();
        }, 2000); // Hide tooltip after 2 seconds
        return false;
    }

    // Continue with form submission or next step
    // For example:
    // window.location.href = url;

    loadNextStep(partialName);
}

//goal update
function selectGoal(button) {
    const goalValue = button.getAttribute('data-goal');
    document.getElementById('goal').value = goalValue;
    document.getElementById('goals').value = goalValue;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.goal').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');
    if (goalValue == "Maintain Weight") {
        var div = document.getElementById('targetweight');
        var divv = document.getElementById('hidethis');
        div.classList.add('hide');
        divv.classList.add('hide');

        const weight = document.getElementById('weightss').value;
        document.getElementById('targetweight').value = weight;

    }
    else {
        var div = document.getElementById('targetweight');
        var divv = document.getElementById('hidethis');
        div.classList.remove('hide');
        divv.classList.remove('hide');
    }
    
}

function selectBarriers(button) {
    // Toggle 'btn-selected' class for the clicked button
    button.classList.toggle('btn-selected');
}

function selectDiatpref(button) {
    const goalValue = button.getAttribute('data-pref');
    document.getElementById('Diatpref').value = goalValue;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.dietpref').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');
}

function selectActivity(button) {
    const ActivityLevel = button.getAttribute('data-actlevel');
    document.getElementById('ActivityLevel').value = ActivityLevel;
    document.getElementById('activelevel').value = ActivityLevel;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.selected').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');

}
function selectWeeklyGoal(button) {
    const ActivityLevel = button.getAttribute('data-weekgoal');
    document.getElementById('WeeklyGoal').value = ActivityLevel;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.selected').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');

    var amount;

    document.getElementById('weekgoalname').innerText = ActivityLevel;

    if (document.getElementById('goals').value == "Lose Weight") {
        document.getElementById('lessmore').innerText = "Less";
    } else {
        document.getElementById('lessmore').innerText = "more";
    }

    if (ActivityLevel == "Relaxed") {
        document.getElementById('weekgoalamnt').innerText = 250;
        document.getElementById('visibles').classList.add('hide');
    }
    else if (ActivityLevel == "Normal") {
        document.getElementById('weekgoalamnt').innerText = 500;
        document.getElementById('visibles').classList.add('hide');
    }
    else if (ActivityLevel == "Strict") {
        document.getElementById('weekgoalamnt').innerText = 1000;
        document.getElementById('visibles').classList.remove('hide');
    }
    

    document.getElementById('visible').classList.remove('hide');
}

function updateGoal() {
    var goal = document.getElementById('goals').value;
    document.getElementById('goal-text').innerText = goal;
    document.getElementById('goal-texts').innerText = goal;
}

function selectNoOfMeal(button) {
    const Meals = button.getAttribute('data-noofmeal');
    document.getElementById('mealamount').value = Meals;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.selected').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');

}



//BMI Display Graph
function BMIGraph() {
    var bmiInput = document.getElementById("bmi");
    var bmi = parseFloat(bmiInput.value);
    document.getElementById('BMIvalue').innerText = bmi;
    var marker = document.getElementById("bmiMarker");
    var bar = document.getElementById("bmiBar");
    var barWidth = bar.offsetWidth;
    var position = (bmi / 40) * barWidth; // Assuming 40 is the maximum BMI value on the scale
    marker.style.left = position + "px";
}

function actleve() {
    var actlevel = document.getElementById("activelevel").value;
    document.getElementById('actlevel').innerText = actlevel;

    if (actlevel == "Sedentary") {
        document.getElementById('actlevelsub').innerText = "Little to no exercise";
    }
    if (actlevel == "Lighly active") {
        document.getElementById('actlevelsub').innerText = "Light exercise / sports 1-2 days per week";
    }
    if (actlevel == "Moderately active") {
        document.getElementById('actlevelsub').innerText = "Moderate exercise / sports 3-4 days per week";
    }
    if (actlevel == "Very active") {
        document.getElementById('actlevelsub').innerText = "Hard exercise / sports 6-7 days per week";
    }
    if (actlevel == "Highly active") {
        document.getElementById('actlevelsub').innerText = "Hard daily exercise / sports & physical job";
    }


}
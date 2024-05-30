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

    // Validate target weight input
    if (targetWeightValue === "" || targetWeightValue <= 0) {
        let heightTooltip = new mdb.Tooltip(targetWeightInput, {
            title: "Please enter a valid Weight",
            placement: 'bottom'
        });
        heightTooltip.show();

        // Optionally clear height input and hide tooltip after a delay
        setTimeout(() => {
            targetWeightInput.value = '';
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
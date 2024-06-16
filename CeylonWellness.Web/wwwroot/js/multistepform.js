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

    document.getElementById('form-container').addEventListener('submit', function (e) {
        e.preventDefault();
        var partialName = $(e.target).data('next-step');
        loadNextStep(partialName);
    });
    const selectedGender = sessionStorage.getItem('selectedGender');
    if (selectedGender === 'Female') {
        document.getElementById('hip-label').style.display = 'block';
        document.getElementById('hip').style.display = 'block';
    }
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
                const selectedGender = sessionStorage.getItem('selectedGender');
                if (selectedGender === 'Female') {
                    document.getElementById('hip-label').style.display = 'block';
                    document.getElementById('hip').style.display = 'block';
                }
                
            });
        },
        error: function () {
            // Handle potential errors here (e.g., display an error message)
            console.error("Error saving or loading partial view.");
        }
    });
}

//function ageVerification(partialName) {
//    const ageInput = document.getElementById("Age");
//    const age = ageInput.value;

//    // Age Validation
//    if (age.trim() === "" || age <= 0) {
//        // Create a Bootstrap tooltip
//        let tooltip = new mdb.Tooltip(ageInput, {
//            title: "Please enter a valid age",
//            placement: 'bottom'
//        });
//        tooltip.show();

//        // Optionally clear input and hide tooltip after a delay
//        setTimeout(() => {
//            ageInput.value = '';
//            tooltip.hide();
//        }, 2000); // Hide tooltip after 2 seconds

//        return; // Don't continue if invalid
//    }
//    loadNextStep(partialName);
//}
function ageVerification(partialName) {
    const ageInput = document.getElementById("Age");
    const age = parseInt(ageInput.value);

    document.getElementById("AgeBMR").value = age;
    // Age Validation
    if (isNaN(age) || age < 18 || age > 50) {
        // Create a Bootstrap tooltip
        let tooltip = new mdb.Tooltip(ageInput, {
            title: "Age must be between 18 and 50",
            placement: 'bottom'
        });
        tooltip.show();

        // Optionally clear input and hide tooltip after a delay
        setTimeout(() => {
            tooltip.hide();
        }, 2000); // Hide tooltip after 2 seconds

        return; // Don't continue if invalid
    }
    loadNextStep(partialName);
}

function setGender(gender) {
    document.getElementById('gender').value = gender;
    // Save the gender selection in localStorage or sessionStorage
    sessionStorage.setItem('selectedGender', gender);
    // Redirect to the next step
    loadNextStep('Steps/_setAge');

}

//function WHVerification(partialName) {
//    const weightInput = document.getElementById("weight");
//    const heightInput = document.getElementById("height");

//    const weight = weightInput.value;
//    const height = heightInput.value;

//    let bmi = calculateBMI(weight, height);

//    document.getElementById('bmi').value = bmi;

//    if (weight.trim() === "" || weight <= 0) {
//        // Create a Bootstrap tooltip for weight input
//        let weightTooltip = new mdb.Tooltip(weightInput, {
//            title: "Please enter a valid weight",
//            placement: 'bottom'
//        });
//        weightTooltip.show();

//        // Optionally clear weight input and hide tooltip after a delay
//        setTimeout(() => {
//            //weightInput.value = '';
//            weightTooltip.hide();
//        }, 2000); // Hide tooltip after 2 seconds

//        return; // Don't continue if weight is invalid
//    }

//    if (height.trim() === "" || height <= 0) {
//        // Create a Bootstrap tooltip for height input
//        let heightTooltip = new mdb.Tooltip(heightInput, {
//            title: "Please enter a valid height",
//            placement: 'bottom'
//        });
//        heightTooltip.show();

//        // Optionally clear height input and hide tooltip after a delay
//        setTimeout(() => {
//            //heightInput.value = '';
//            heightTooltip.hide();
//        }, 2000); // Hide tooltip after 2 seconds

//        return; // Don't continue if height is invalid
//    }

//    document.getElementById('weightss').value = weight;

//    // If both weight and height are valid, proceed to the next step
//    loadNextStep(partialName);
//}
function WHVerification(partialName) {
    const weightInput = document.getElementById("weight");
    const heightInput = document.getElementById("height");

    const weight = parseFloat(weightInput.value);
    const height = parseFloat(heightInput.value);

    if (isNaN(weight) || weight < 15 || weight > 300) {
        showErrorTooltip(weightInput, "Weight should be between 15 and 300 kg");
        return; // Don't continue if weight is invalid
    }

    if (isNaN(height) || height < 100 || height > 300) {
        showErrorTooltip(heightInput, "Height should be between 100 and 300 cm");
        return; // Don't continue if height is invalid
    }
    document.getElementById('WeightBMR').value = weight;

    document.getElementById("HeightBMR").value = height;
    // If both weight and height are valid, calculate BMI
    const bmi = calculateBMI(weight, height);
    document.getElementById('bmi').value = bmi;
    document.getElementById('weightss').value = weight;

    // Proceed to the next step
    loadNextStep(partialName);
}

function showErrorTooltip(element, message) {
    let tooltip = new mdb.Tooltip(element, {
        title: message,
        placement: 'bottom'
    });
    tooltip.show();

    // Optionally clear input and hide tooltip after a delay
    setTimeout(() => {
        tooltip.hide();
    }, 2000); // Hide tooltip after 2 seconds
}

function NWHVerification(partialName) {
    const neckInput = document.getElementById("neck");
    const waistInput = document.getElementById("waist");
    const hipInput = document.getElementById("hip");

    const neck = parseFloat(neckInput.value);
    const waist = parseFloat(waistInput.value);
    const hip = hipInput ? parseFloat(hipInput.value) : null;

    const selectedGender = sessionStorage.getItem('selectedGender');

    let isValid = true;

    // Validate Neck
    if (neckInput.value.trim() === "" || neck < 30 || neck > 60) {
        showErrorTooltip(neckInput, "Neck measurement should be between 30 and 60 cm");
        isValid = false;
    }

    // Validate Waist
    if (waistInput.value.trim() === "" || waist < 50 || waist > 150) {
        showErrorTooltip(waistInput, "Waist measurement should be between 50 and 150 cm");
        isValid = false;
    }

    // Neck should be less than Waist
    if (neck >= waist) {
        showErrorTooltip(neckInput, "Neck measurement should be less than Waist measurement");
        isValid = false;
    }

    // Validate Hip if gender is Female
    if (selectedGender === 'Female' && (hipInput && (hipInput.value.trim() === "" || hip < 50 || hip > 150))) {
        showErrorTooltip(hipInput, "Hip measurement should be between 50 and 150 cm");
        isValid = false;
    }


    if (isValid) {
        loadNextStep(partialName);
    }
}



function showErrorTooltip(element, message) {
    let tooltip = new mdb.Tooltip(element, {
        title: message,
        placement: 'bottom'
    });
    tooltip.show();

    // Optionally clear input and hide tooltip after a delay
    setTimeout(() => {
        tooltip.hide();
    }, 2000); // Hide tooltip after 2 seconds
}
function showErrorTooltip(inputElement, message) {
    let tooltip = new mdb.Tooltip(inputElement, {
        title: message,
        placement: 'bottom'
    });
    tooltip.show();

    setTimeout(() => {
        tooltip.hide();
    }, 2000);
}

// BMI Calculator
function calculateBMI(weight, height) {
    if (height <= 0 || weight <= 0) {
        return "Height and weight must be greater than zero";
    }
    let heightInMeters = height / 100;
    let bmi = weight / (heightInMeters * heightInMeters);
    return bmi.toFixed(2);
}
function TargetWeightVerification(partialName) {
    const targetWeightInput = document.getElementById('targetweight');
    const targetWeightValue = targetWeightInput.value;

    const weight = document.getElementById('weightss').value;
    const goalinput = document.getElementById('goal');
    const goal = document.getElementById('goals').value;

    document.getElementById('TWeightBMR').value = targetWeightValue;

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
//function selectGoal(button) {
//    const goalValue = button.getAttribute('data-goal');
//    document.getElementById('goal').value = goalValue;
//    document.getElementById('goals').value = goalValue;

//    // Remove 'selected' class from all buttons
//    document.querySelectorAll('.goal').forEach(btn => btn.classList.remove('btn-selected'));

//    // Add 'selected' class to the clicked button
//    button.classList.add('btn-selected');
//    if (goalValue == "Maintain Weight") {
//        var div = document.getElementById('targetweight');
//        var divv = document.getElementById('hidethis');
//        div.classList.add('hide');
//        divv.classList.add('hide');

//        const weight = document.getElementById('weightss').value;
//        document.getElementById('targetweight').value = weight;

//    }
//    else {
//        var div = document.getElementById('targetweight');
//        var divv = document.getElementById('hidethis');
//        div.classList.remove('hide');
//        divv.classList.remove('hide');
//    }
//}
function selectGoal(button) {
    const goalValue = button.getAttribute('data-goal');
    document.getElementById('goal').value = goalValue;
    document.getElementById('goals').value = goalValue;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.goal').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');

    const targetWeightContainer = document.getElementById('targetWeightContainer');
    const targetWeightInput = document.getElementById('targetweight');
    const hideThisDiv = document.getElementById('hidethis');

    if (goalValue == "Maintain Weight") {
        targetWeightContainer.classList.add('hide');
        hideThisDiv.classList.add('hide');

        const weight = document.getElementById('weightss').value;
        targetWeightInput.value = weight;
    } else {
        targetWeightContainer.classList.remove('hide');
        hideThisDiv.classList.remove('hide');
    }
}

    function selectBarriers(button) {
        // Toggle 'btn-selected' class for the clicked button
        button.classList.toggle('btn-selected');
}
function selectWheyproteinpref(button) {
    const wheyproteinpref = button.getAttribute('data-pref');
    document.getElementById('wheyproteinpref').value = wheyproteinpref;
    document.getElementById('wheyproteinprefs').value = wheyproteinpref;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.wheyproteinpref').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');

}
function selectMealinpref(button) {
    const mealintakepref = button.getAttribute('data-pref');
    document.getElementById('mealintakepref').value = mealintakepref;
    document.getElementById('mealintakeprefs').value = mealintakepref;

    document.getElementById('ActlevelBMR').value = ActivityLevel;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.mealintakepref').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');

}

function selectMacroinpref(button) {
    const macropref = button.getAttribute('data-pref');
    document.getElementById('macropref').value = macropref;
    document.getElementById('macroprefs').value = macropref;

    // Remove 'selected' class from all buttons
    document.querySelectorAll('.macropref').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');

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

function setDairy(value) {
    document.getElementById('dairy').value = value;
    document.getElementById('dairy-yes-btn').classList.remove('btn-primary');
    document.getElementById('dairy-no-btn').classList.remove('btn-primary');
    document.getElementById('dairy-yes-btn').classList.add('btn-outline-primary');
    document.getElementById('dairy-no-btn').classList.add('btn-outline-primary');
    if (value === 'Yes') {
        document.getElementById('dairy-yes-btn').classList.add('btn-primary');
        document.getElementById('dairy-yes-btn').classList.remove('btn-outline-primary');
    } else {
        document.getElementById('dairy-no-btn').classList.add('btn-primary');
        document.getElementById('dairy-no-btn').classList.remove('btn-outline-primary');
    }
}

function setEggs(value) {
    document.getElementById('eggs').value = value;
    document.getElementById('eggs-yes-btn').classList.remove('btn-primary');
    document.getElementById('eggs-no-btn').classList.remove('btn-primary');
    document.getElementById('eggs-yes-btn').classList.add('btn-outline-primary');
    document.getElementById('eggs-no-btn').classList.add('btn-outline-primary');
    if (value === 'Yes') {
        document.getElementById('eggs-yes-btn').classList.add('btn-primary');
        document.getElementById('eggs-yes-btn').classList.remove('btn-outline-primary');
    } else {
        document.getElementById('eggs-no-btn').classList.add('btn-primary');
        document.getElementById('eggs-no-btn').classList.remove('btn-outline-primary');
    }
}

// Function to handle selection of dietary preference
function selectDiatpref(button) {
    const prefValue = button.getAttribute('data-pref');
    document.getElementById('Diatpref').value = prefValue;
    document.getElementById('Diatprefs').value = prefValue;
    // Remove 'selected' class from all buttons
    document.querySelectorAll('.dietpref').forEach(btn => btn.classList.remove('btn-selected'));

    // Add 'selected' class to the clicked button
    button.classList.add('btn-selected');
   
}
function handleNextStep() {
    // Check if a dietary preference is selected
    const selectedDiat = document.getElementById('Diatprefs').value;
    if (selectedDiat) {
        // Depending on the selected preference, load the appropriate next step
        if (selectedDiat === 'Vegetarian') {
            loadNextStep('Steps/_DairyProductsEggs'); // Load DairyProductsEggs for vegetarians
        } else {
            loadNextStep('Steps/_MeatPref'); // Load MeatPref for non-vegetarians
        }
    } else {
        // Inform the user to select a preference before proceeding
        alert('Please select a dietary preference before continuing.');
    }
}
document.addEventListener('DOMContentLoaded', function () {
    // This function runs when the DOM is fully loaded
    populateMacroOptions(); // Call function to populate options based on initial goal
});
function populateMacroOptions() {
    const goalElement = document.getElementById('goals');
    const selectedDiet = document.getElementById('Diatprefs').value;

    if (!goalElement) {
        console.error('Goal element not found!');
        return;
    }

    const goal = goalElement.value;

    // Helper functions to show or hide options
    function showOption(optionClass) {
        const elements = document.getElementsByClassName(optionClass);
        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.remove('hide'); // Adjust display style as needed
        }
    }

    function hideOption(optionClass) {
        const elements = document.getElementsByClassName(optionClass);
        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.add('hide');
        }
    }
    function selectOption(optionId) {
        const element = document.getElementById(optionId);
        if (element) {
            element.classList.add('selected'); // Adjust class or styling as needed
        }
    }
    console.log(goal)
    
    
    // Show or hide options based on the selected goal using if-else statements
    if (goal == 'Lose Weight') {
        showOption('budgetPlan');
        showOption('lowCarb');
        showOption('balanced');
        showOption('highProtein');
    } else if (goal == 'Maintain Weight') {
        showOption('budgetPlan');
        hideOption('lowCarb');
        showOption('balanced');
        showOption('highProtein');
    } else if (goal == 'Gain Weight') {
        console.log("Gain weight is selected")
        showOption('budgetPlan');
        hideOption('lowCarb'); // Hide 'lowCarb' option when gaining weight
        showOption('balanced');
        showOption('highProtein');
    } else {
        console.error('Invalid goal:', goal);
        // show all options or hide all options
    }
    // Auto-select 'Value Protein Plan (Budget plan)' if diet is 'Vegetarian'
    if (selectedDiet === 'Vegetarian') {
        selectOption('budgetPlan');
    }
    
}
function handleMacroNextStep() {
    const selectedDiat = document.getElementById('Diatprefs').value;

    if (selectedDiat) {
        const selectedGoal = document.getElementById('goals').value; // Ensure this element exists
        populateMacroOptions(); // Populate options based on selected goal // Populate options based on selected goal
        if (selectedDiat === 'Non Vegetarian') {
            loadNextStep('Steps/_SelectMacroType'); // Load DairyProductsEggs for vegetarians
        } else if(selectedDiat === 'Vegetarian')  {
            loadNextStep('Steps/_ActivityLevel'); // Load MeatPref for non-vegetarians
            //loadNextStep('Steps/_SelectMacroType');
        }
    } else {
        alert('Please select a diet');
    }
}
function populateMealOptions() {
    const goalElement = document.getElementById('goals');
    const selectedDiet = document.getElementById('Diatprefs').value;
    var actlevel = document.getElementById("activelevel").value;

    if (!actlevel) {
        console.error('Activity element not found!');
        return;
    }

    const goal = goalElement.value;

    // Helper functions to show or hide options
    function showOption(optionClass) {
        const elements = document.getElementsByClassName(optionClass);
        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.remove('hide'); // Adjust display style as needed
        }
    }

    function hideOption(optionClass) {
        const elements = document.getElementsByClassName(optionClass);
        for (let i = 0; i < elements.length; i++) {
            elements[i].classList.add('hide');
        }
    }
    function selectOption(optionId) {
        const element = document.getElementById(optionId);
        if (element) {
            element.classList.add('selected'); // Adjust class or styling as needed
        }
    }
    console.log(goal)


    // Show or hide options based on the selected goal using if-else statements
    if (goal == 'Lose Weight' && actlevel == "Sedentary") {
        showOption('twomeal');
        showOption('threemeal');
        hideOption('threemealonesnack');
        hideOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Lose Weight' && actlevel == "Lighly active") {
        showOption('twomeal');
        showOption('threemeal');
        showOption('threemealonesnack');
        hideOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Lose Weight' && actlevel == "Moderately active") {
        hideOption('twomeal');
        hideOption('threemeal');
        showOption('threemealonesnack');
        hideOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Lose Weight' && actlevel == "Very active") {
        hideOption('twomeal');
        hideOption('threemeal');
        showOption('threemealonesnack');
        showOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Lose Weight' && actlevel == "Highly active") {
        hideOption('twomeal');
        hideOption('threemeal');
        hideOption('threemealonesnack');
        showOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    //} else if (goal == 'Gain Weight') {
    //    console.log("Gain weight is selected")
    //    showOption('budgetPlan');
    //    hideOption('lowCarb'); // Hide 'lowCarb' option when gaining weight
    //    showOption('balanced');
    //    showOption('highProtein');
    }else if (goal == 'Maintain Weight' && actlevel == "Sedentary") {
        hideOption('twomeal');
        showOption('threemeal');
        hideOption('threemealonesnack');
        hideOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Maintain Weight' && actlevel == "Lighly active") {
        hideOption('twomeal');
        showOption('threemeal');
        showOption('threemealonesnack');
        hideOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Maintain Weight' && actlevel == "Moderately active") {
        hideOption('twomeal');
        hideOption('threemeal');
        showOption('threemealonesnack');
        hideOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Maintain Weight' && actlevel == "Very active") {
        hideOption('twomeal');
        hideOption('threemeal');
        showOption('threemealonesnack');
        showOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Maintain Weight' && actlevel == "Highly active") {
        hideOption('twomeal');
        hideOption('threemeal');
        hideOption('threemealonesnack');
        showOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    }
    else if (goal == 'Gain Weight' && actlevel == "Sedentary") {
        hideOption('twomeal');
        showOption('threemeal');
        showOption('threemealonesnack');
        hideOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Gain Weight' && actlevel == "Lighly active") {
        hideOption('twomeal');
        hideOption('threemeal');
        showOption('threemealonesnack');
        showOption('threemealtwosnack');
        hideOption('threemealthreesnack');
    } else if (goal == 'Gain Weight' && actlevel == "Moderately active") {
        hideOption('twomeal');
        hideOption('threemeal');
        hideOption('threemealonesnack');
        showOption('threemealtwosnack');
        showOption('threemealthreesnack');
    } else if (goal == 'Gain Weight' && actlevel == "Very active") {
        hideOption('twomeal');
        hideOption('threemeal');
        hideOption('threemealonesnack');
        hideOption('threemealtwosnack');
        showOption('threemealthreesnack');
    } else if (goal == 'Gain Weight' && actlevel == "Highly active") {
        hideOption('twomeal');
        hideOption('threemeal');
        hideOption('threemealonesnack');
        hideOption('threemealtwosnack');
        showOption('threemealthreesnack');
    }
    else {
        console.error('Invalid goal:', goal);
        // show all options or hide all options
    }

}

function calculateCalorieNeeds(gender, weight, height, age, activityLevel) {
    let BMR;

    // Calculate BMR based on gender
    if (gender === 'Male') {
        BMR = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
    } else if (gender === 'Female') {
        BMR = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age);
    } else {
        throw new Error('Invalid gender. Please specify "male" or "female".');
    }

    // Determine the activity factor
    let activityFactor;
    switch (activityLevel) {
        case 'Sedentary':
            activityFactor = 1.2;
            break;
        case 'Lighly active':
            activityFactor = 1.375;
            break;
        case 'Moderately active':
            activityFactor = 1.55;
            break;
        case 'Very active':
            activityFactor = 1.725;
            break;
        case 'Highly active':
            activityFactor = 1.9;
            break;
        default:
            throw new Error('Invalid activity level. Please specify one of the following: sedentary, lightly active, moderately active, very active, super active.');
    }

    // Calculate TDEE
    const TDEE = BMR * activityFactor;
    return TDEE;
}
function BMISummaryOnLoad() {
    const gender = document.getElementById("GenderBMR").value;
    const weight = document.getElementById("WeightBMR").value;
    const height = document.getElementById("HeightBMR").value;
    const age = document.getElementById("AgeBMR").value;
    const activityLevel = document.getElementById("ActlevelBMR").value;

    const dailyCalories = calculateCalorieNeeds(gender, weight, height, age, activityLevel);
    const dacal = Math.round(dailyCalories);
    document.getElementById('dailycalorie').innerText = dacal;

    document.getElementById('MaintaincalAmount').value = dacal;
}




}

// BMR and TDEE

function calculateCalorieNeeds(gender, weight, height, age, activityLevel) {
    let BMR;

    // Calculate BMR based on gender
    if (gender === 'Male') {
        BMR = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
    } else if (gender === 'Female') {
        BMR = 447.593 + (9.247 * weight) + (3.098 * height) - (4.330 * age);
    } else {
        throw new Error('Invalid gender. Please specify "male" or "female".');
    }

    // Determine the activity factor
    let activityFactor;
    switch (activityLevel) {
        case 'Sedentary':
            activityFactor = 1.2;
            break;
        case 'Lighly active':
            activityFactor = 1.375;
            break;
        case 'Moderately active':
            activityFactor = 1.55;
            break;
        case 'Very active':
            activityFactor = 1.725;
            break;
        case 'Highly active':
            activityFactor = 1.9;
            break;
        default:
            throw new Error('Invalid activity level. Please specify one of the following: sedentary, lightly active, moderately active, very active, super active.');
    }

    // Calculate TDEE
    const TDEE = BMR * activityFactor;
    return TDEE;
}

function BMISummaryOnLoad() {
    const gender = document.getElementById("GenderBMR").value;
    const weight = document.getElementById("WeightBMR").value;
    const height = document.getElementById("HeightBMR").value;
    const age = document.getElementById("AgeBMR").value;
    const activityLevel = document.getElementById("ActlevelBMR").value;

    const dailyCalories = calculateCalorieNeeds(gender, weight, height, age, activityLevel);
    const dacal = Math.round(dailyCalories);
    document.getElementById('dailycalorie').innerText = dacal;

    document.getElementById('MaintaincalAmount').value = dacal;
}


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

function setGender(gender) {
    document.getElementById('gender').value = gender;
    // Save the gender selection in localStorage or sessionStorage
    sessionStorage.setItem('selectedGender', gender);
    // Redirect to the next step
    loadNextStep('Steps/_setAge'); // Ensure this step is correct
}

function WHVerification(partialName) {
    const weightInput = document.getElementById("weight");
    const heightInput = document.getElementById("height");

    const weight = weightInput.value;
    const height = heightInput.value;

    let bmi = calculateBMI(weight, height);

    document.getElementById('bmi').value = bmi;

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
function NWHVerification(partialName) {
    const neckInput = document.getElementById("neck");
    const waistInput = document.getElementById("waist");
    const hipInput = document.getElementById("hip");

    const neck = neckInput.value;
    const waist = waistInput.value;
    const hip = hipInput ? hipInput.value : null;

    const selectedGender = sessionStorage.getItem('selectedGender');

    let isValid = true;

    // Validate Neck
    if (neck.trim() === "" || neck <= 0) {
        showErrorTooltip(neckInput, "Please enter a valid neck measurement");
        isValid = false;
    }

    // Validate Waist
    if (waist.trim() === "" || waist <= 0) {
        showErrorTooltip(waistInput, "Please enter a valid waist measurement");
        isValid = false;
    }


    // Validate Hip if gender is female
    if (selectedGender === 'Female' && (hip.trim() === "" || hip <= 0)) {
        showErrorTooltip(hipInput, "Please enter a valid hip measurement");
        isValid = false;
    }

    if (isValid) {
        loadNextStep(partialName);
    }
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

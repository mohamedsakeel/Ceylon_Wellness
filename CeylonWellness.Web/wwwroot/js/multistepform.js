//function loadNextStep(partialName) {

//    const ageInput = document.getElementById("Age");
//    const age = ageInput.value;

//    // Age Validation
//    if (age.trim() === "" || age < 0) {
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

//    // Serialize form data from the current step
//    var data = $('#multistepform').serialize();

//    $.ajax({
//        type: "POST",
//        url: '/Form/SaveStepData',
//        data: data,
//        success: function () {
//            // Load the next partial view
//            $("#form-container").load('/Form/LoadPartial?partialName=' + partialName);
//        },
//        error: function () {
//            // Handle potential errors here (e.g., display an error message)
//            console.error("Error saving or loading partial view.");
//        }
//    });
//}

function loadNextStep(partialName) {
    const ageInput = document.getElementById("Age");
    const age = ageInput.value;

    // Age Validation
    if (age.trim() === "" || age < 0) {
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

    // Set gender
    const genderInput = document.getElementById("gender");
    if (genderInput.value.trim() === "") {
        console.error("Gender not selected.");
        return; // Don't continue if gender not selected
    }

    // Serialize form data from the current step
    var data = $('#multistepform').serialize();

    $.ajax({
        type: "POST",
        url: '/Form/SaveStepData',
        data: data,
        success: function () {
            // Load the next partial view
            $("#form-container").load('/Form/LoadPartial?partialName=' + partialName);
        },
        error: function () {
            // Handle potential errors here (e.g., display an error message)
            console.error("Error saving or loading partial view.");
        }
    });
}
function setBarriers(barrier) {
    var barriersInput = document.getElementById('barriers');
    var currentBarriers = barriersInput.value.split(',').filter(Boolean); // Get current barriers as an array
    var barrierIndex = currentBarriers.indexOf(barrier);

    if (barrier === 'other') {
        var otherInput = document.getElementById('otherInput');
        if (barrierIndex === -1) {
            otherInput.style.display = 'block';
        } else {
            otherInput.style.display = 'none';
            document.getElementById('otherText').value = ''; // Clear other text when deselecting "Other"
        }
    }

    if (barrierIndex === -1) {
        // If the barrier is not selected, add it
        currentBarriers.push(barrier);
    } else {
        // If the barrier is already selected, remove it
        currentBarriers.splice(barrierIndex, 1);
    }

    // Update the hidden input field with the selected barriers
    barriersInput.value = currentBarriers.join(',');
}
function setGender(gender) {
    document.getElementById("gender").value = gender;
}

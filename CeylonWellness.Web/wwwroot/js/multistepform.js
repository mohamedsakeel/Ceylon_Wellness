function loadNextStep(partialName) {

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

function ageVerification(partialName) {
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

    loadNextStep(partialName);
}
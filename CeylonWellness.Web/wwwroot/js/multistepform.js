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
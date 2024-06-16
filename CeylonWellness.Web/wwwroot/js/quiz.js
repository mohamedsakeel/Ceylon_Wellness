


function checkAnswer() {
    var selectedOption = document.querySelector('input[name="quizOptions"]:checked');
    var correctAnswer = document.getElementById('correctAnswer').value - 1;
    var resultDiv = document.getElementById('result');
    if (selectedOption) {
        if (selectedOption.value == correctAnswer) {
            resultDiv.innerHTML = '<div class="alert alert-success">Correct!</div>';
        } else {
            resultDiv.innerHTML = '<div class="alert alert-danger">Incorrect. The correct answer is option ' + (parseInt(correctAnswer) + 1) + '.</div>';
        }
    } else {
        resultDiv.innerHTML = '<div class="alert alert-warning">Please select an option.</div>';
    }
}

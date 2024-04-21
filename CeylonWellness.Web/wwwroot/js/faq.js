$(document).ready(function () {
    $('#sendButton').click(function () {
        var question = $('#userInput').val();
        if (question.trim() !== '') {
            sendMessage(question);
        }
    });

    $('#userInput').keypress(function (e) {
        if (e.which === 13) { // Enter key pressed
            var question = $('#userInput').val();
            if (question.trim() !== '') {
                sendMessage(question);
            }
        }
    });
});

function sendMessage(question) {
    $('#messages').append('<div class="message user-message"><img src="/Assets/Male User.png" class="profile-pic"> ' + question + '</div>');
    $('#userInput').val('');

    $.ajax({
        type: 'POST',
        url: '/FAQ/Query',
        data: { question: question },
        success: function (response) {
            $('#messages').append('<div class="message bot-message"><img src="/Assets/Bot.png" class="profile-pic"> ' + response.answer + '</div>');
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
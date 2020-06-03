// https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-3.1
'use strict';
(function() {
    const $connect = document.getElementById("connect");
    const $messages = document.getElementById("messages");
    const connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:7002/messaging')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    $connect.onclick = function() {
        appendMessage("Connecting to Messaging Hub...");
        connection.start()
            .then(() => {
            console.log('Now connected'); 
            appendMessage("Connected.", "primary");
        })
        .catch(err => appendMessage(err,"danger"));
    }

    connection.on('game_event_source_added', (operation) => {
        appendMessage('Game Event Source Added.', "success", operation);
    });

    
    function appendMessage(message, type, data) {
        var dataInfo = "";
        if (data) {
            dataInfo += "<div>" + JSON.stringify(data) + "</div>";
        }
        $messages.innerHTML += `<li class="list-group-item list-group-item-${type}">${message} ${dataInfo}</li>`;
    }
})();
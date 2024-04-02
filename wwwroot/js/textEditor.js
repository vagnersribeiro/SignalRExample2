"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/textEditorHub")
    .withAutomaticReconnect([0, 0, 10000])
    .build();

document.getElementById("textEditor").disabled = true;

connection.on("ReceiveChanges", function (user, newText) {
    document.getElementById("textEditor").value = newText;
});

connection.start().then(function () {
    document.getElementById("textEditor").disabled = false;
    connection.invoke("CreateOrGetExisting", 1);
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("textEditor").addEventListener("input", function (event) {
    var user = "nada";
    var newText = document.getElementById("textEditor").value;

    connection.invoke("SendChanges", user, newText).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
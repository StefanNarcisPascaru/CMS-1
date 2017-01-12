"use strict";

function generateOption(ranks) {
    var form = document.getElementById("role");
    var children = form.childNodes;
    children.forEach(function (child) {
        if (child.nodeName === "INPUT" && child.getAttribute("type") === "checkbox") {
            ranks.forEach(function (rank) {
                if (child.getAttribute("value") === rank.name) {
                    child.setAttribute("checked", "");
                    document.getElementById(rank.name).value = rank.name;
                }
            });
        }
    });
};

function check(role) {
    var inputElem = document.getElementById(role);
    if (inputElem.value === role) {
        inputElem.value = "";
    } else {
        inputElem.value = role;
    }
}

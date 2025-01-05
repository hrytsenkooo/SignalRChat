document.addEventListener("DOMContentLoaded", function () {
    window.elementExists = function (elementId) {
        return document.getElementById(elementId) !== null;
    };

    window.scrollToBottom = function (elementId) {
        var element = document.getElementById(elementId);
        if (element && element.scrollHeight > 0) {
            setTimeout(() => {
                element.scrollTop = element.scrollHeight;
            }, 300);
        }
    };
});
var check = document.getElementById("check");
var cross = document.getElementById("cross");

document.addEventListener("DOMContentLoaded", function () {
    check.style.display = "none";
});

function greenCheck() {
    if (check.style.display === "none") {
        check.style.display = "block";
        cross.style.display = "none";
    } else {
        check.style.display = "none";
        cross.style.display = "block";
    }
}
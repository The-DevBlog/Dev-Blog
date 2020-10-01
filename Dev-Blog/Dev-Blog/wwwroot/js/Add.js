//TODO: not working
function greenCheck() {
    var check = document.getElementById("check");
    var cross = document.getElementById("cross");
    if (check.style.display === "none") {
        check.style.display = "block";
        cross.style.display = "none";
    } else {
        check.style.display = "none";
        cross.style.display = "block";
    }
}
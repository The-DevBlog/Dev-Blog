function SubmitForm(button) {
    document.getElementById(button).submit();
}

function AddDisplay(elements, dis) {
    var tags = document.querySelectorAll(elements);
    tags.forEach(x => x.style.display = dis);
}

function RemoveDisplay(elements) {
    var tags = document.querySelectorAll(elements);
    tags.forEach(x => x.style.display = "none");
}

function HideTag(elements) {
    var tags = document.querySelectorAll(elements);
    tags.forEach(x => x.style.visibility = "hidden");
}

function ShowTag(elements) {
    var tags = document.querySelectorAll(elements);
    tags.forEach(x => x.style.visibility = "visible");
}
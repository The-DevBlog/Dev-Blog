function SubmitForm(button) {
    document.getElementById(button).submit();
}

function ShowTag(elements, dis) {
    var tags = document.querySelectorAll(elements);
    tags.forEach(x => x.style.display = dis);
}

function HideTag(elements) {
    var tags = document.querySelectorAll(elements);
    tags.forEach(x => x.style.display = "none");
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//for adding and removing a requirement
document.addEventListener("DOMContentLoaded", function () {
    var addRequirementButton = document.getElementById("add-requirement");
    var requirementsWrapper = document.querySelector(".requirements-wrapper");

    function addRequirement() {
        var requirement = document.createElement("div");
        requirement.className = "requirement";
        var input = document.createElement("input");
        input.type = "text";
        input.className = "requirement-input";
        input.name = "job-requirement[]";
        input.required = true;
        var removeButton = document.createElement("button");
        removeButton.type = "button";
        removeButton.textContent = "-";
        removeButton.className = "remove-requirement";
        removeButton.addEventListener("click", removeRequirement);
        requirement.appendChild(input);
        requirement.appendChild(removeButton);
        requirementsWrapper.appendChild(requirement);
    }

    function removeRequirement() {
        this.parentNode.parentNode.removeChild(this.parentNode);
    }

    addRequirementButton.addEventListener("click", addRequirement);
});

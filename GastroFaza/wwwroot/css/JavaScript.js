const selectList = document.getElementById("nationality");
const selectedValue = document.querySelector(".select_list_nationality .selected-value");

selectList.addEventListener("change", () => {
    selectedValue.textContent = selectList.value;
});
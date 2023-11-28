(function() {
  const selectElement = $("#selectLanguage select");
  // Attach event listener to the select element
  selectElement.on("change", function() {
    // Submit the parent form
    selectElement.parent().trigger("submit");
  });
})();

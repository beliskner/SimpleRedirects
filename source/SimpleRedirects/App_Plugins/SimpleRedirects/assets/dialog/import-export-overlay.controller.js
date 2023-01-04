angular.module('umbraco').controller('SimpleRedirects.ImportExportController', function($scope, editorService, $routeParams, fileManager, notificationsService){
    let amountOfRedirects = $scope.model;
    const vm = this;

    vm.fileUpload = null;
    vm.validationLoading = false;
    vm.exportButtonState = 'init';
    vm.importing = false;

    vm.export = function exportToCsv() {
        vm.exportButtonState = 'busy';
        location.href = "/umbraco/backoffice/SimpleRedirects/RedirectApi/Export?dataRecordProvider=Csv";
        vm.exportButtonState = 'success';
    }

    vm.close = parent.close;
    function init() {
        console.log(amountOfRedirects)
        // Create the file upload property editor

        // Mark the property as mandatory
/*        vm.fileUpload.validation = { mandatory: true };

        // Only allow .xlsx and .csv files
        vm.fileUpload.config = {
            fileExtensions: [{ value: 'csv' }]
        };*/

/*        // Whenever the currency is updated, refresh the table
        $scope.$watch('vm.fileUpload.value', function(newValue, oldValue) {
            // Make sure the value is updated, mostly to prevent double API call on initial page load
            if (newValue === oldValue || !newValue) {
                return;
            }

            vm.validate();
        });*/
    }

    init();
});
angular.module('umbraco').controller('SimpleRedirects.ImportExportController', function($scope, Upload, localizationService, umbRequestHelper, overlayService, mediaHelper, mediaTypeHelper){
    let amountOfRedirects = $scope.model;
    const vm = this;

    vm.exportButtonState = 'init';
    vm.importing = false;
    vm.file = null;
    vm.overwriteMatches = false;

    vm.exportFileName = {
        text: '',
        regex: '/[^a-z0-9_\\-]/gi',
        placeholder: 'Optionally type a filename'
    }

    vm.export = function exportToCsv() {
        // console.log(vm.exportFileName.text);
        vm.exportButtonState = 'busy';
        location.href = "/umbraco/backoffice/SimpleRedirects/RedirectApi/Export?dataRecordProvider=Csv";
        vm.exportButtonState = 'success';
    }

    vm.import = function importRedirects() {
        if (vm.file !== null) {
            Upload.upload({
                url: "backoffice/SimpleRedirects/RedirectApi/Import?overwriteMatches=" + vm.overwriteMatches,
                file: vm.file
            });
        }
    }

    vm.close = parent.close;
    function init() {
        console.log(amountOfRedirects)
    }

    $scope.handleFile = function ($file) {
        console.log($file);
        vm.file = $file;
    };

    init();
});
/// <reference path="../../angular.js/angular.js" />
departmentApp.controller('departmentController', function ($scope, $timeout, departmentService) {
    $scope.newDepartment = {};
    $scope.message = "";
    $scope.btnName = "Add Department";
    DepartmentList();
    function DepartmentList() {
        departmentService.departmentList().then(function (result) {
            $scope.departments = result.data;
        })
    }
    $scope.Insert = function () {
        if ($scope.newDepartment.DepartmentId > 0) {
            departmentService.updateDepartment($scope.newDepartment).then(function (result) {
                    $scope.Message = "Data updated successfully";
                    $scope.departments = result.data;
                    DepartmentList();
                    $scope.btnName = "Add Department";
                    ClearAll();
                });
        } else {
            departmentService.insert($scope.newDepartment).then(function (result) {
                $scope.message = "Building Save Succfully";
                ClearAll();
                DepartmentList();
            });
        }
    }
    $scope.ClearMessage = function () {
        $scope.message = "";
    }
    function ClearAll() {
        $scope.newDepartment = null;
    }
    $scope.SelectedDepartment = function (department) {
        $scope.clickedDepartment = department;
    };
    $scope.SelectedDepartmentForEdit = function (department) {
        $scope.newDepartment = department;
        $scope.btnName = "Update";
    }
    $scope.DeleteDepartment = function (id) {
        departmentService.deleteDepartment(id).then(function (result) {
            $scope.Message = "Data deleted successfully"
            DepartmentList();
        });
    };
});


employeeApp.controller('employeeController', function ($scope, Upload, $timeout, employeeService) {
    $scope.newEmployee = {};
    $scope.message = "";
    $scope.UploadFiles = function (files) {
        $scope.SelectedFiles = files;
    };
    FillDropDownValue();
    EmployeeList();
    function EmployeeList() {
        employeeService.employeeList().then(function (result) {
            $scope.employees = result.data;
        })
    }
    $scope.Insert = function () {
        employeeService.insert($scope.newEmployee).then(function (returnId) {
            if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                UploadFile(returnId.data);
            }
        }).then(function (result) {
            $scope.message = "Employee Save Succfully";
            ClearAll();
            EmployeeList();
        });
    }
    $scope.ClearMessage = function () {
        $scope.message = "";
    }
    $scope.UpdateEmployee = function () {
        employeeService.updateEmployee($scope.clickedEmployee).then(function (returnId) {
            if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                UploadFile(returnId.data);
            }
        }).then(function (result) {
            $scope.Message = "Data updated successfully";
            $scope.building = result.data;
            EmployeeList();
        });
    };
    function UploadFile(id) {
        Upload.upload({
            url: "api/FileUpload/EmployeeImageUpload?id=" + id,
            data: {
                files: $scope.SelectedFiles
            }
        }).then(function (response) {
            $timeout(function () {
                $scope.Result = response.data;
                EmployeeList();
            });
        }, function (response) {
            if (response.status > 0) {
                var errorMsg = response.status + ': ' + response.data;
                alert(errorMsg);
            }
        });
    }
    function ClearAll() {
        $scope.newEmployee = null;
        $(".change_image").val('');
        $('.change_edit').attr('src', 'images/no-image.png');
    }
    $scope.SelectedEmployee = function (employee) {
        $scope.clickedEmployee = employee;
    };
    $scope.DeleteEmployee = function (id) {
        employeeService.deleteEmployee(id).then(function () {
            $scope.Message = "Data deleted successfully"
            EmployeeList();
        });
    };
    function FillDropDownValue() {
        employeeService.departmentList().then(function (result) {
            $scope.departments = result.data;
        });
    }
});
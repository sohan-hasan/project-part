departmentApp.service('departmentService', function ($http) {
    this.departmentList = function () {
        var response = $http.get('Department/GetAll');
        return response;
    };
    this.insert = function (department) {
        var response = $http({
            method: "Post",
            url: "Department/Create",
            data: department,
            contentType: false,
            processData: false
        });
        return response;
    };
    this.updateDepartment = function (department) {
        var response = $http({
            method: 'post',
            url: 'Department/Edit',
            data: department,
            contentType: false,
            processData: false
        });
        return response;
    };
    this.deleteDepartment = function (id) {
        var response = $http({
            method: 'post',
            url: "Department/Delete?id=" + id,
            params: { DepartmentId: JSON.stringify(id) }
        });
        return response;
    }
});

employeeApp.service('employeeService', function ($http) {
    this.employeeList = function () {
        var response = $http.get('Employee/GetAll');
        return response;
    };
    this.insert = function (employee) {
        var response = $http({
            method: "Post",
            url: "Employee/Create",
            data: employee,
            contentType: false,
            processData: false
        });
        return response;
    };
    this.updateEmployee = function (employee) {
        var response = $http({
            method: 'post',
            url: 'Employee/Edit',
            data: employee,
            contentType: false,
            processData: false
        });
        return response;
    };
    this.deleteEmployee = function (id) {
        var response = $http({
            method: 'post',
            url: "Employee/Delete?id=" + id,
            params: { EmployeeId: JSON.stringify(id) }
        });
        return response;
    };
    this.departmentList = function () {
        var response = $http.get('Department/GetAll');
        return response;
    };
});
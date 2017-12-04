var crmApp = angular.module('crmApp', []);


crmApp.controller('UserController', ['$scope', '$rootScope', '$http', '$filter', function ($scope, $rootScope, $http, $filter) {
    var url = $("#base_url").val();
    var userID = $("#user_id").val();

    var key = "_user_";

    $scope.getUser = function () {
        if (sessionStorage.getItem(key) == null) {
            $http.get(url + 'Account/GetUser', {
                params: { userID: userID }
            })
            .success(function (response) {
                sessionStorage.setItem(key, JSON.stringify(response));
                $scope.user = response;
            })
            .error(function (response) {
                alert(response);
            });

        }
        else {
            $scope.user = JSON.parse(sessionStorage.getItem(key));
        }
    }

}]);

crmApp.controller('SearchController', ['$scope', '$rootScope', '$http', '$filter', function ($scope, $rootScope, $http, $filter) {
    $scope.keyPressed = function () {
        //if ($scope.customerNo.length != 9) {
        //    alert("Invalid number entered");
        //}
        //else {
        $rootScope.$broadcast('cmnoSelected', $scope.customerNo);
        //}
    }
}]);

crmApp.controller('AccessListController', ['$scope', '$rootScope', '$http', '$filter', function ($scope, $rootScope, $http, $filter) {
    var url = $("#base_url").val();
    var userID = $("#user_id").val();

    $scope.$on('accessListUpdated', function (events, flag) {
        $scope.getList();
    })

    $scope.getList = function () {
        $http.get(url + 'AccessLog/List', {
            params: { userID: userID, count: 10 }
        })
        .success(function (response) {
            $scope.accessList = response;
            angular.forEach(response, function (value, key) {
                var _date = new Date(parseInt(value.DateTime.substr(6)));
                value.DateTime = _date.toLocaleDateString() + " " + _date.toLocaleTimeString()
            });
        })
        .error(function (response) {
            alert(response);
        });
    }

    $scope.select = function(data) {
        $rootScope.$broadcast('cmnoSelected', data.CustomerNo);
    }
}]);

crmApp.controller('CustomerController',  ['$scope', '$rootScope', '$http', '$filter', function ($scope, $rootScope, $http, $filter) {
    $scope.loaded = false;
    $scope.productsloaded = false;
    $scope.serviceResponse = 0;
    $scope.uiResponseStart = new Date();
     
    var key_cmno = "_cmno_";

    var url = $("#url_path").val();
    var baseurl = $("#base_url").val();
    var userID = $("#user_id").val();

    if (localStorage.getItem(key_cmno) == null) {
        $scope.selectedCmno = "000000094"; //for demo only
    }
    else {
        $scope.selectedCmno = localStorage.getItem(key_cmno);
    }

    $scope.$on('cmnoSelected', function (events, cmno) {
        localStorage.setItem(key_cmno, cmno);
        $scope.selectedCmno = cmno;

        $scope.uiResponseStart = new Date();
        $scope.getCustomer();
    })

   
    //Customer details
    $scope.getCustomer = function () {
        var key = "_customer_" + $scope.selectedCmno;
        $scope.serviceResponse = 0;

        if (sessionStorage.getItem(key) == null) {
            $('#pluswrap').show();
            $http.get(url + '/GetInfo', {
                params: { customerNo: $scope.selectedCmno }
            })
               .success(function (response) {

                   $scope.loaded = true;
                   $('#pluswrap').hide();
                   $scope.customer = response;

                   //calculate ui response
                   var uiResponseEnd = new Date();
                   $scope.renderTime = $scope.uiResponseStart.getTime() - uiResponseEnd.getTime();
                   $scope.serviceResponse = response.ServiceResponseTime;

                   sessionStorage.setItem(key, JSON.stringify(response));

                   $scope.updateLog($scope.customer);
                   $scope.getProfile();
                   
               })
               .error(function (response) {
                   $('#pluswrap').hide();
                   $scope.loaded = true;
                   alert(response);
               });
        }
        else {
            $scope.customer = JSON.parse(sessionStorage.getItem(key));
            $scope.loaded = true;
            $('#pluswrap').hide();

            var uiResponseEnd = new Date();
            $scope.renderTime = $scope.uiResponseStart.getTime() - uiResponseEnd.getTime();

            $scope.updateLog($scope.customer);
            $scope.getProfile();
        }
    }

    $scope.executeAsync = function () {
        $scope.productsloaded = true;
       // $scope.getProducts();
        //$scope.getAlerts();
        //$scope.getLeads();

    }


    $scope.updateLog = function (customer) {
        var data = {};
        data.CustomerName = customer.FullNameEnglish;
        data.CustomerNo = $scope.selectedCmno; 
        data.UserID = userID;

        $http.post(baseurl + 'AccessLog/Add', JSON.stringify(data))
       .success(function (response) {
           $rootScope.$broadcast('accessListUpdated', true);
       })
       .error(function (response) {
           alert(response);
       });

    }

    $scope.getProfile = function () {
        var key = "_profile_" + $scope.selectedCmno;

        if (sessionStorage.getItem(key) == null) {
            $http.get(url + '/GetProfile', {
                params: { customerNo: $scope.selectedCmno }
            })
            .success(function (response) {
                sessionStorage.setItem(key, JSON.stringify(response));
                $scope.financialProfile = response;
                $scope.executeAsync();
            })
            .error(function (response) {
                alert(response);
            });
        }
        else {
            $scope.financialProfile = JSON.parse(sessionStorage.getItem(key));
            $scope.executeAsync();
        }
    }

    //Customer active products
    $scope.getProducts = function () {
        $scope.products = [];
        $scope.productsloaded = false;

        $http.get(url + '/GetProducts', {
            params: { customerNo: $scope.selectedCmno }
        })
        .success(function (response) {
            $scope.products = response;
            $scope.productsloaded = true;
        })
        .error(function (response) {
            alert(response);
        });
    }

    //Get customer alerts
    $scope.getAlerts = function () {
        var key = "_alerts_" + $scope.selectedCmno;
        $scope.alerts = [];
        if (sessionStorage.getItem(key) == null) {
            $http.get(url + '/GetAlerts', {
                params: { customerNo: $scope.selectedCmno }
            })
            .success(function (response) {
                sessionStorage.setItem(key, JSON.stringify(response));
                $scope.alerts = response;
            })
            .error(function (response) {
                alert(response);
            });
        }
        else {
            $scope.alerts = JSON.parse(sessionStorage.getItem(key));
        }
    }

    //Get campaign leads
    $scope.getLeads = function () {
        var key = "_leads_" + $scope.selectedCmno;
        $scope.leads = {};
        if (sessionStorage.getItem(key) == null) {
            $http.get(url + '/GetCampaignLeads', {
                params: { customerNo: $scope.selectedCmno }
            })
            .success(function (response) {
                sessionStorage.setItem(key, JSON.stringify(response));
                $scope.leads = response;
            })
            .error(function (response) {
                alert(response);
            });
        }
        else {
            $scope.leads = JSON.parse(sessionStorage.getItem(key));
        }
    }

    //Customer account details
    $scope.getAccountDetails = function (accountNo) {
        $http.get(url + '/AccountDetails', {
            params: { accountNo: accountNo }
        })
        .success(function (response) {
            $scope.account = response;
            $('#accountDialog').modal('show');
        })
        .error(function (response) {
            alert(response);
        });
    }


    $scope.productDetails = function (product) {
        if (product.ProductCategory == "Deposit Account") {
            $scope.getAccountDetails(product.ProductNumber);
        }
        else {
            alert("Not implemented");
        }
    }

    //Simulate brodcast message when customer is updated
    $scope.simulateUpdate = function () {
        jQuery(sendNotification("_customer_", $scope.selectedCmno, userID));
    }
    $scope.getCustomer();

}]);

crmApp.controller('LeadMonitorController', ['$scope', '$rootScope', '$http', '$filter', function ($scope, $rootScope, $http, $filter) {
    var url = $("#url_path").val();
    var userID = $("#user_id").val();
    $scope.search = {};

    $scope.user = JSON.parse(sessionStorage.getItem("_user_"));

    //Get instant leads
    $scope.getMonitor = function () {
        var key = "_leads_" + $scope.selectedCmno;
        $scope.leads = {};
        if (sessionStorage.getItem(key) == null) {
            $http.get(url + '/GetLeadsMonitor', {
                params: { userID: userID }
            })
            .success(function (response) {
                sessionStorage.setItem(key, JSON.stringify(response));
                $scope.leads = response;
            })
            .error(function (response) {
                alert(response);
            });
        }
        else {
            $scope.leads = JSON.parse(sessionStorage.getItem(key));
        }
    }

    $scope.filterByStatus = function (flag) {
        switch (flag) {
            case 1:
                $scope.search.Contacted = 1;
                break;
            case 2:
                $scope.search.Contacted = 0;
                break;
            case 3:
                $scope.search.Closed = 1;
                break;
            case 4:
                $scope.search.Closed = 0;
                break;
            case 0:
                $scope.search = {};
                break;
        }
    }

}]);

crmApp.controller('IBGCustomerController', ['$scope', '$rootScope', '$http', '$filter', function ($scope, $rootScope, $http, $filter) {
    $scope.loaded = false;
    $scope.productsloaded = false;
    $scope.showHistory = false;
    $scope.serviceResponse = 0;
    $scope.uiResponseStart = new Date();
    $scope.country = "BH";
    $scope.currency = "BHD";
    var key_cmno = "_ibg_cmno_";

    var url = $("#url_path").val();
    var baseurl = $("#base_url").val();

    if (localStorage.getItem(key_cmno) == null) {
        $scope.selectedCmno = "100002"; //for demo only
    }
    else {
        $scope.selectedCmno = localStorage.getItem(key_cmno);
    }

    $scope.$on('cmnoSelected', function (events, cmno) {
        localStorage.setItem(key_cmno, cmno);
        $scope.selectedCmno = cmno;
        $scope.uiResponseStart = new Date();
        $scope.showHistory = false;
        $scope.getCustomer();
    })


    //Customer details
    $scope.getCustomer = function () {
        $scope.serviceResponse = 0;
        $scope.showHistory = false;
            $('#pluswrap').show();
            $http.get(url + '/CustomerInquiry', {
                params: { customerNo: $scope.selectedCmno, country: $scope.country }
            })
               .success(function (response) {
                   $scope.loaded = true;
                   $('#pluswrap').hide();
                   $scope.customer = response;
                   $scope.serviceResponse = response.ResponseTime;
                   $scope.getProducts();
              })
               .error(function (response) {
                   $('#pluswrap').hide();
                   $scope.loaded = true;
                   alert(response);
               });
    }

    $scope.getProducts = function () {
        $scope.products = [];
        $scope.productsloaded = false;
        $scope.portfolioResponse = 0;
        $http.get(url + '/PortfolioInquiry', {
            params: { customerNo: $scope.selectedCmno, country: $scope.country, currency: $scope.currency }
        })
        .success(function (response) {
            $scope.products = response.Accounts;
            $scope.productsloaded = true;
            $scope.portfolioResponse = response.ResponseTime;
        })
        .error(function (response) {
            alert(response);
        });
    }


    $scope.showTransactions = function (accountNo) {
        $scope.transactions = [];
        $scope.transactionsResponse = 0;
        $http.get(url + '/TransactionHistory', {
            params: { accountNo: accountNo, country: $scope.country, currency: $scope.currency }
        })
        .success(function (response) {
            alert(response);
            $scope.transactions = response.Transactions;
            // $scope.transactionsResponse = response.ResponseTime;
            $scope.showHistory = true;
        })
        .error(function (response) {
            alert(response);
        });
    }

    $scope.hideHistory = function () {
        $scope.showHistory = false;
    }
   
    $scope.getCustomer();

}]);

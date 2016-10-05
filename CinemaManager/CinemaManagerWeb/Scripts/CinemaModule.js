angular.module('Cinema', [])
.controller('demo', function($scope, $http)
{
    $http.get('http://localhost:63426/api/Cinemas').then(function(response)
    {
        
        $scope.cinemas = response.data;
    })
})


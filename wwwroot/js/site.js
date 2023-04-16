// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Function that generates the pie chart via the c3.js library
$(function () {
    $.ajax({
        type: "GET",
        url: 'Home/EmployeeModelJson'
    }).done(function (data) {
        var chartData = [];
        var value = [];
        data.forEach(function (d) {
            chartData[d.name] = d.totalTimeWorked;
            value.push(d.name);
        });
        c3.generate({
            bindto: '#chart',
            data: {
                json: [chartData],
                keys: {
                    value: value
                },
                type: 'pie'
            }
        });
    });
});

var button = document.getElementById('downloadChart');
var chart = document.getElementById('chart');

// Outputting the pie chart as a png file when button is clicked via html2canvas
button.onclick = function () {
    html2canvas(chart).then(function (canvas) {
        var img = canvas.toDataURL("image/png");
        var link = document.createElement('a');
        link.download = 'chartimage.png';
        link.href = img;
        link.click();
    });
}





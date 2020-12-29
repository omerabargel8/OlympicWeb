function GetBest() {
    var element = document.getElementById("omer");
    let movies = [];


    console.log("SAPIRRRRRRR");
    let xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState === 4) {
            if (this.status === 200) {
                let feedPost = JSON.parse(this.responseText);
                let listOfPosts = [];
                let post1 = feedPost[0];
                console.log(feedPost);
                console.log(post1.content);
                element.textContent = post1.content;
                //$("#omer").append("<p>" + post1.content + "</p>");

            } else {
                console.log("Error", xhttp.statusText);
                alert(xhttp.statusText);
            }
        }
    };
    xhttp.open("GET", "https://localhost:44328/api/Feed", true);
    xhttp.send();
}






/**
var element = document.getElementById("omer");
let movies = [];

element.textContent = "BBBBBBB";
Url = "../api/Feed"
$.getJSON(Url, function () {
  //showFlightDetails(id, data.passengers, data.company_name, data.initial_location.date_time, data.segments);
  //drawFlightRoute(id, data.initial_location, data.segments);
  console.log(data.Content);
  console.log("BLAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
});
element.textContent = "KK";

/**
fetch('https://localhost:44328/api/Feed')
  .then(console.log(data));
  */
/**
fetch('https://localhost:44328/api/Feed')
    .then(response => response.json())
    .then(data => console.log(data));
    */


/**
  $.getJSON('https://localhost:44328/api/Feed', function (data) {
      //element.textContent = data.Content
      data.forEach(function (Post) {
          console.log(Post.Content);
          element.textContent = flight.Content;
      });
  });*/

/**
Url = "https://localhost:44328/api/Feed"
$.getJSON(Url, function (data) {
    data.forEach(function (post) {
        console.log("blaa");
        element.textContent = "blaa";
    });
});*/
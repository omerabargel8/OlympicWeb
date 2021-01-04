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
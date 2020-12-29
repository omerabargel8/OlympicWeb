function GetBest() {
    var element = document.getElementById("omer");
    let movies = [];
    fetch('https://localhost:44328/api/Feed')
        .then(response => response.json())
        .then(data => {
            this.setState({ movies: data })
        })
    console.log("blaa");

    /**
    $.getJSON('https://localhost:44328/api/Feed', function (data) {
        //element.textContent = data.Content
        data.forEach(function (Post) {
            console.log(Post.Content);
            element.textContent = flight.Content;
        });
    });*/
}
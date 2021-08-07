// content.js
//alert("Hello from your Chrome extension!")

// content.js
//var firstHref = $("a[href^='http']").eq(4).attr("href");

//console.log(firstHref);

// content.js
chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
    if( request.message === "clicked_browser_action_old" ) {
      var firstHref = $("a[href^='http']").eq(0).attr("href");

      console.log(firstHref);
    }
  }
);

// content.js
chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
    if( request.message === "clicked_browser_action" ) {
      var firstHref = $("a[href^='http']").eq(0).attr("href");

      console.log(firstHref);

      // This line is new!
      chrome.runtime.sendMessage({"message": "open_new_tab", "url": firstHref});
    }
  }
);

//alert("siemka");

$('a[aria-label]').each(function(i, el) {
	//todo: pokazanie wyniku
	
    //console.log(i + " " + $(this).attr('aria-label'));
	var movieTitle = $(this).attr('aria-label');
	//$(this).parent().text(movieTitle);
	//$(this).parent().css( "background-color", "red" );
	$(this).parent().append(function(){
		//return "<p>" + movieTitle + "</p>";
	});
	
	var movieTitleForGoogle = movieTitle.replace(" ", "+");
	//console.log(movieTitleForGoogle);
	var elementID = this.id;//$(this).attr('id');
	var o = $(this).parent();
	//console.log($(this));
	//console.log(i +' ' +el);
	
	$.ajax({
		url:'https://localhost:44370/movie?movieTitle=' + movieTitle,
        type:'GET',
        crossDomain: true,
		"headers": {
              "accept": "application/json",
              "Access-Control-Allow-Origin":"*"
          },
        success: function(data){
			var rat = data[0].ratingStr;
			console.log(el + ' ' + rat);
			console.log(elementID);
			console.log(o);
			o.append(function(){
				return "<p>" + rat + "</p>";
			});
			//$('#content').html(o);
		}
		});
		
		$(this).parent().append(function(){
				//return "<p>" + ratG + "</p>";
			});
			
	/*$.ajax({
    url:'https://www.google.pl/search?q=' + //moneyball+imdb' +
		movieTitle+"+imdb",
        type:'GET',
		//type: "POST",
		 dataType: 'jsonp',
        crossDomain: true,
		"headers": {
              "accept": "application/json",
              "Access-Control-Allow-Origin":"*"//"x-requested-with"
          },
        dataType: "json",
        success: function(data){
           //$('#content').html($(data).find('#firstHeading').html());
		   console.log($(data).find("span:contains('Ocena:')"));
        }
	});*/
});


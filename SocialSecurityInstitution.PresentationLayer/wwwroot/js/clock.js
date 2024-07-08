(function ($) {
  if (!window.requestAnimationFrame) {
    window.requestAnimationFrame = window.mozRequestAnimationFrame ||
      window.webkitRequestAnimationFrame ||
      window.msRequestAnimationFrame ||
      window.oRequestAnimationFrame ||
      function (cb) { setTimeout(cb, 1000/60); };
  }

  var $h = $("#hour"),
      $m = $("#minute"),
      $s = $("#second");

  function computeTimePositions($h, $m, $s) {
    var now = new Date(),
        h = now.getHours(),
        m = now.getMinutes(),
        s = now.getSeconds(),
        ms = now.getMilliseconds(),
        degS, degM, degH;

    degS = (s * 6) + (6 / 1000 * ms);
    degM = (m * 6) + (6 / 60 * s) + (6 / (60 * 1000) * ms);
    degH = (h * 30) + (30 / 60 * m);

    $s.css({ "transform": "rotate(" + degS + "deg)" });
    $m.css({ "transform": "rotate(" + degM + "deg)" });
    $h.css({ "transform": "rotate(" + degH + "deg)" });

    requestAnimationFrame(function () {
      computeTimePositions($h, $m, $s);
    });
  }

  function setUpFace() {
    for (var x = 1; x <= 60; x += 1) {
      addTick(x); 
    }

    function addTick(n) {
      var tickClass = "smallTick",
          tickBox = $("<div class=\"faceBox\"></div>"),
          tick = $("<div></div>"),
          tickNum = "";

      if (n % 5 === 0) {
        tickClass = (n % 15 === 0) ? "largeTick" : "mediumTick";
        tickNum = $("<div class=\"tickNum\"></div>").text(n / 5).css({ "transform": "rotate(-" + (n * 6) + "deg)" });
        if (n >= 50) {
          tickNum.css({"left":"-0.5em"});
        }
      }


      tickBox.append(tick.addClass(tickClass)).css({ "transform": "rotate(" + (n * 6) + "deg)" });
      tickBox.append(tickNum);

      $("#clock").append(tickBox);
    }
  }

  function setSize() {
    var b = $(this), //html, body
        w = b.width(),
        x = Math.floor(w / 30) - 1,
        px = (x > 25 ? 26 : x) + "px";

    $("#clock").css({"font-size": px });
    
    if (b.width() !== 400) {
      setTimeout(function() { $("._drag").hide(); }, 500);
    }
  }

  $(document).ready(function () {
    setUpFace();
    computeTimePositions($h, $m, $s);
    $("section").on("resize", setSize).trigger("resize");
    $("section").resizable({handles: 'e'});    
  });
}(jQuery));

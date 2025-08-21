
/*
$(document).ready(function(){

  var highestBox = 0;
      $('.services-items').each(function(){  
              if($(this).height() > highestBox){  
              highestBox = $(this).height();  
      }
  });    
  $('.services-items').height(highestBox);
  
  });*/


  $(document).ready(function(){
    $(".navbar-toggler").click(function(){
      $(".main-nav").toggleClass("toggle");
      
    });
  });

  

  $('.navbar-toggler').click(function() {
    $('.xx').toggleClass('fa-bars');
    $('.xx').toggleClass('fa-times'); });


    $(document).ready(function () {

      var highestBox = 0;
      $('.app-item').each(function () {
        if ($(this).height() > highestBox) {
          highestBox = $(this).height();
        }
      });
      $('.app-item').height(highestBox);
    
    });


var $logoScrollDelay = 0;
$(window).scroll(function (e) {

    if ($(document).scrollTop() >= 20) {
        $('.move-top').css('display', 'block');
       $('.logo').addClass('logo-scroll');
       $('.main-nav').addClass('scroll-bg');
       
        /* $('.nav-section .navbar-nav').addClass('nav-scroll');
       
        $('.nav-section .navbar-brand').css('display', 'block');
        $('.nav-section').addClass('shadow-scroll');
        $('.menu-icon').css('padding-top','12px');
        $('#content ').addClass('menu-bg');*/


        

    } else {
      
        $('.move-top').css('display', 'none');
        $('.main-nav').removeClass('scroll-bg');
        $('.logo').removeClass('logo-scroll');
       /* $('#content ').removeClass('menu-bg');
        $('header').removeClass('scroll-menu-bg');
       
        $('.nav-section .navbar-brand').css('display', 'none');
        $('.nav-section').removeClass('shadow-scroll');
        $('.menu-icon').css('padding-top','20px')*/





    }
});




$(".move-top").click(function () {
    $(window).scrollTop(0);
});



$(document).ready(function() {
  // When the page has loaded
 /* $(".loading-page").css('display','none');*/
 /* $("body").css('overflow','auto');*/

  setTimeout(function(){ $(".loading-page").fadeOut("slow");$("body").css('overflow','auto'); }, 2500);
});




 /***********************whats*********************/
 (function ($) {
  var wa_time_out, wa_time_in;
  $(document).ready(function () {
    $(".wa__btn_popup").on("click", function () {
      if ($(".wa__popup_chat_box").hasClass("wa__active")) {
        $(".wa__popup_chat_box").removeClass("wa__active");
        $(".wa__btn_popup").removeClass("wa__active");
        clearTimeout(wa_time_in);
        if ($(".wa__popup_chat_box").hasClass("wa__lauch")) {
          wa_time_out = setTimeout(function () {
            $(".wa__popup_chat_box").removeClass("wa__pending");
            $(".wa__popup_chat_box").removeClass("wa__lauch");
          }, 400);
        }
      } else {
        $(".wa__popup_chat_box").addClass("wa__pending");
        $(".wa__popup_chat_box").addClass("wa__active");
        $(".wa__btn_popup").addClass("wa__active");
        clearTimeout(wa_time_out);
        if (!$(".wa__popup_chat_box").hasClass("wa__lauch")) {
          wa_time_in = setTimeout(function () {
            $(".wa__popup_chat_box").addClass("wa__lauch");
          }, 100);
        }
      }
    });

    function setCookie(cname, cvalue, exdays) {
      var d = new Date();
      d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
      var expires = "expires=" + d.toUTCString();
      document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    function getCookie(cname) {
      var name = cname + "=";
      var ca = document.cookie.split(";");
      for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == " ") {
          c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
          return c.substring(name.length, c.length);
        }
      }
      return "";
    }

    $("#nta-wa-gdpr").change(function () {
      if (this.checked) {
        setCookie("nta-wa-gdpr", "accept", 30);
        if (getCookie("nta-wa-gdpr") != "") {
          $('.nta-wa-gdpr').hide(500);
          $('.wa__popup_content_item').each(function () {
            $(this).removeClass('pointer-disable');
            $('.wa__popup_content_list').off('click');
          })
        }
      }
    });

    if (getCookie("nta-wa-gdpr") != "") {
      $('.wa__popup_content_list').off('click');
    } else {
      $('.wa__popup_content_list').click(function () {
        $('.nta-wa-gdpr').delay(500).css({ "background": "red", "color": "#fff" });
      });
    }
  });
})(jQuery);

/****************whats*********************************/

  




(function() {
  // Init
  var container = document.getElementById("container"),
      inner = document.getElementById("inner");

  // Mouse
  var mouse = {
    _x: 0,
    _y: 0,
    x: 0,
    y: 0,
    updatePosition: function(event) {
      var e = event || window.event;
      this.x = e.clientX - this._x;
      this.y = (e.clientY - this._y) * -1;
    },
    setOrigin: function(e) {
      this._x = e.offsetLeft + Math.floor(e.offsetWidth / 2);
      this._y = e.offsetTop + Math.floor(e.offsetHeight / 2);
    },
    show: function() {
      return "(" + this.x + ", " + this.y + ")";
    }
  };

  // Track the mouse position relative to the center of the container.
  mouse.setOrigin(container);

  //----------------------------------------------------

  var counter = 0;
  var refreshRate = 10;
  var isTimeToUpdate = function() {
    return counter++ % refreshRate === 0;
  };

  //----------------------------------------------------

  var onMouseEnterHandler = function(event) {
    update(event);
  };

  var onMouseLeaveHandler = function() {
    inner.style = "";
  };

  var onMouseMoveHandler = function(event) {
    if (isTimeToUpdate()) {
      update(event);
    }
  };

  //----------------------------------------------------

  var update = function(event) {
    mouse.updatePosition(event);
    updateTransformStyle(
      (mouse.y / inner.offsetHeight / 2).toFixed(2),
      (mouse.x / inner.offsetWidth / 2).toFixed(2)
    );
  };

  var updateTransformStyle = function(x, y) {
    var style = "rotateX(" + x + "deg) rotateY(" + y + "deg)";
    inner.style.transform = style;
    inner.style.webkitTransform = style;
    inner.style.mozTranform = style;
    inner.style.msTransform = style;
    inner.style.oTransform = style;
  };

  //--------------------------------------------------------

  container.onmousemove = onMouseMoveHandler;
  container.onmouseleave = onMouseLeaveHandler;
  container.onmouseenter = onMouseEnterHandler;
})();
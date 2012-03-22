
    var apiKey = '012417b9e30c96f340d20fc24274214e';
    var userId = '71353437@N05';

    function showalbums() {
        $.getJSON("http://api.flickr.com/services/rest/?method=flickr.photosets.getList&api_key=" + apiKey + "&user_id=" + userId + "&format=json&jsoncallback=?", function (data) {

            var list = '';

            $.each(data.photosets.photoset, function (i, set) {

                var setPhotoURL = 'http://farm' + set.farm + '.static.flickr.com/' + set.server + '/' + set.primary + '_' + set.secret + '_s.jpg';



                var link = '<a  class="photoalbumlink" ';
                link += 'href="';
                link += 'javascript:showalbum(\''
                link += set.id;
                link += '\')" ';
                link += '>';

                var li = link;
                li += ' <div class="photoalbumdiv" ';
                li += 'id="photoalbumdiv' + i + '" >';



                var img = '<img class="photoalbumimg" ';
                img += 'src="';
                img += setPhotoURL;
                img += '" />';
                li += img;

                li += ' <div class="photoalbumtitle"> ';
                li += set.title._content;
                li += ' </div>';
                li += "</div>";

                li += '</a>';
                //li += link;

                list += li;
                //alert(li);
            });

            //list += '</ul>';
            document.getElementById('contentdiv').innerHTML = "";
            $(list).appendTo("#contentdiv");

            //            var current = 0;
            //            $.each(data.photosets.photoset, function (i, set) {
            //                if (current == 0) {
            //                    current = 1;
            //                    $('#photoalbumdiv' + i).rotate(random(0, 5));
            //                } else {
            //                    current = 0;
            //                    $('#photoalbumdiv' + i).rotate(random(-5, 0));
            //                }
            //            });

        });
    }

    function showalbum(setid) {
        $.getJSON('http://api.flickr.com/services/rest/?format=json&method=' +
        'flickr.photosets.getPhotos&api_key=' + apiKey + '&user_id=' + userId + '&photoset_id=' + setid + '&jsoncallback=?',
    function (data) {

        var classShown = 'class="lightbox"';
        var classHidden = 'class="lightbox hidden"';
        document.getElementById('contentdiv').innerHTML = "";

        $.each(data.photoset.photo, function (i, rPhoto) {
            var basePhotoURL = 'http://farm' + rPhoto.farm + '.static.flickr.com/'
            + rPhoto.server + '/' + rPhoto.id + '_' + rPhoto.secret;

            var thumbPhotoURL = basePhotoURL + '_s.jpg';
            var mediumPhotoURL = basePhotoURL + '.jpg';

            var photoStringStart = '<a class="imageurl" ';
            var photoStringEnd = 'title="' + rPhoto.title + '" href="' +
                mediumPhotoURL + '"><div class="imagediv" id="image' + i + '"><img class="imageimg" src="' + thumbPhotoURL + '" alt="' +
                rPhoto.title + '"/></div></a>;'
            var photoString = photoStringStart + classHidden + photoStringEnd;
            $(photoString).appendTo("#contentdiv");

        });


        var current = 0;
        $.each(data.photoset.photo, function (i, rPhoto) {
            if (current == 0) {
                current = 1;
                $('#image' + i).rotate(random(0, 5));
            } else {
                current = 0;
                $('#image' + i).rotate(random(-5, 0));
            }

        });
        $('.imageurl').lightBox();
    });
    };


//    function hideorshowfooter() {
//        if (show != null && show == 1) 
//        {
//            show = 0;
//        }
//        else 
//        {
//            show = 1;
//        }

//        dohideorshowfooter(show);
//        //createCookie('scoutsfooter', show, 30);
//    }

//    function dohideorshowfooter(doshow) {
//        if (doshow == null) {
//            doshow = 0;
//        }
//        var browserheight = doshow == 1 ? (getBrowserHeight() + 60) : 80;
//        var bottomvalue = doshow == 1 ? 0 : 0;
//        var bottomopacity = doshow == 1 ? 0.5 : 1;
//        var bottomfooteropacity = doshow == 1 ? 1 : 0.5;
//        if (doshow == 1) {
//           // alert('ok');
//            show = 1;
//            $('#footer').animate({ opacity: bottomopacity, bottom: bottomvalue, height: browserheight }, 200,
//                    function () {
//                        $('#footercontent').animate({ opacity: bottomfooteropacity, bottom: 0 }, 200,
//                        function () {
//                            //code if animation is finished
//                        
//                        });
//                    });
//        } else {
//                    show = 0;
//                    //alert('nok');
//                    $('#footercontent').animate({ opacity: bottomfooteropacity, bottom: bottomvalue }, 200,
//                function () {
//                    $('#footer').animate({ opacity: bottomopacity, bottom: bottomvalue, height: browserheight }, 200,
//                    function () {
//                        //code if animation is finished
//                        
//                        $('#footercontent').html('');
//                    });
//                });
//        }
//    }


    jQuery.extend({
        random: function (min, max) {
            return Math.round(min + ((max - min) * (Math.random() % 1)));
        }
    });

    function buildphotoheader() {
        var browserwidth = (getBrowserWidth() - 22);
        var itemcount = Math.floor(browserwidth / 80);
        var imagesdiv = document.getElementById('images');
        imagesdiv.innerHTML = "";
        for (var i = 1; i <= itemcount; i++) {
            var div = document.createElement('div');
            div.setAttribute('class', 'imagediv');
            div.setAttribute('id', 'image' + i);
            var img = document.createElement('img');
            img.setAttribute('alt', '');
            img.setAttribute('src', 'http://farm8.static.flickr.com/7003/6527412107_dc2905fa08_s.jpg');
            img.setAttribute('class', 'imageimg');
            div.appendChild(img);
            imagesdiv.appendChild(div);
        }

        for (var i = 1; i <= itemcount; i++) {
            $('#image' + i).rotate($.random(-15, 15));
        }
    }



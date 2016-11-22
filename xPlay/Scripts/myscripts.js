$(".my_audio").trigger('load');

function play_audio(task) {
    if (task == 'play') {
        $(".my_audio").trigger('play');
    }
    if (task == 'stop') {
        $(".my_audio").trigger('pause');
        $(".my_audio").prop("currentTime", 0);
    }
}

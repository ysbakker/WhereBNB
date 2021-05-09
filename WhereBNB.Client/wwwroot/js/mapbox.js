window.mapbox = {
    init: () => {
        mapboxgl.accessToken = 'pk.eyJ1IjoieW9ycmljayIsImEiOiJja29hOTdiZ3MwNDlvMndvZGEwYzdqdGF6In0.DRd1vqbAUBiSpMymkFNRpw';
        new mapboxgl.Map({
            container: 'mapboxMap',
            style: 'mapbox://styles/yorrick/ckoaamj2t67mq18tefempkihb',
            center: [4.902318081500628, 52.37851665631294],
            zoom: 12
        });
    }
}
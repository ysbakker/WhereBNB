let map;

window.mapbox = {
    init: (dotnetReference, dataUrl) => {
        mapboxgl.accessToken = 'pk.eyJ1IjoieW9ycmljayIsImEiOiJja29hOTdiZ3MwNDlvMndvZGEwYzdqdGF6In0.DRd1vqbAUBiSpMymkFNRpw';
        map = new mapboxgl.Map({
            container: 'mapboxMap',
            style: 'mapbox://styles/yorrick/ckoaamj2t67mq18tefempkihb',
            center: [4.902318081500628, 52.37851665631294],
            zoom: 12
        });
        map.on('load', () => {
            map.addSource('listings', {
                type: 'geojson',
                data: dataUrl,
                cluster: true,
                clusterMaxZoom: 14,
                clusterRadius: 50
            });
            map.addLayer({
                id: 'clusters',
                type: 'circle',
                source: 'listings',
                filter: ['has', 'point_count'],
                paint: {
                    'circle-color': [
                        'step',
                        ['get', 'point_count'],
                        '#BBC8CA',
                        100,
                        '#E0CA3C',
                        750,
                        '#F0544F'
                    ],
                    'circle-radius': [
                        'step',
                        ['get', 'point_count'],
                        20,
                        100,
                        30,
                        750,
                        40
                    ]
                }
            });

            map.addLayer({
                id: 'cluster-count',
                type: 'symbol',
                source: 'listings',
                filter: ['has', 'point_count'],
                layout: {
                    'text-field': '{point_count_abbreviated}',
                    'text-font': ['DIN Offc Pro Medium', 'Arial Unicode MS Bold'],
                    'text-size': 12
                }
            });
            
            map.addLayer({
                id: 'points',
                type: 'circle',
                filter: ['!', ['has', 'point_count']],
                source: 'listings',
                paint: {
                    'circle-color': '#444',
                    'circle-radius': 6,
                    // 'circle-stroke-width': 1,
                    // 'circle-stroke-color': '#fff'
                }
            });

            map.on('click', 'points', function (e) {
                const id = e.features[0].properties.Id;
                dotnetReference.invokeMethodAsync("PointClicked", id);
            });
            
            map.on('mouseenter', 'points', function () {
                map.getCanvas().style.cursor = 'pointer';
            });
            
            map.on('mouseleave', 'points', function () {
                map.getCanvas().style.cursor = '';
            });
        })
    },
    updateData: url => {
        map.getSource('listings').setData(url);
    }
}
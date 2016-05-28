#!/usr/bin/python

from obspy.core.stream import Stream, read
from obspy.core.util import gps2DistAzimuth


host = 'http://examples.obspy.org/'
# Archivos (fmt: SAC)
datos = ['TOK.2011.328.21.10.54.OKR01.HHN.inv',
         'TOK.2011.328.21.10.54.OKR02.HHN.inv',
         'TOK.2011.328.21.10.54.OKR03.HHN.inv',
         'TOK.2011.328.21.10.54.OKR04.HHN.inv',
         'TOK.2011.328.21.10.54.OKR05.HHN.inv',
         'TOK.2011.328.21.10.54.OKR06.HHN.inv',
         'TOK.2011.328.21.10.54.OKR07.HHN.inv',
         'TOK.2011.328.21.10.54.OKR08.HHN.inv',
         'TOK.2011.328.21.10.54.OKR09.HHN.inv',
         'TOK.2011.328.21.10.54.OKR10.HHN.inv']
# epicentro
eq_lat = 35.565
eq_lon = -96.792

# leer las formas de onda
# sumar en un solo datos como estructura

st = Stream()
for formas_ondas in datos:
    st += read(host + formas_ondas)

# calculamos distancia de cabezales codigo analisis sismologico en lat/long
# (trace.stats.sac.stla and trace.stats.sac.stlo)

for tr in st:
    tr.stats.distance = gps2DistAzimuth(tr.stats.sac.stla, tr.stats.sac.stlo,
                                        eq_lat, eq_lon)[0]
    # Configuramos el nombre de la red sismica
    tr.stats.network = 'TOK'

st.filter('bandpass', freqmin=0.1, freqmax=10)

# Ploteamos
st.plot(type='section', plot_dx=20e3, recordlength=100,
        time_down=True, linewidth=.25, grid_linewidth=.25)

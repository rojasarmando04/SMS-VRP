<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logistica.aspx.cs" Inherits="Logistica.Page.Logistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row col-12 p-5 content m-0">
        <asp:UpdatePanel runat="server" class="col-6">
            <ContentTemplate>
                <div class="row col-12 sub-content">
                    <div class="col-12">
                        <h3>Mueva el símbolo del mapa para cargar la longitud y la latitud de la ubicación de la parada</h3>
                        <div class="form-row pt-4">
                            <div class="col-12 p-3">
                                <label for="inptAgrCodAct">Longitud</label>
                                <input type="text" class="form-control input-form col-12 text-center" title="Se requiere este campo" disabled runat="server" id="longitud" placeholder="-84.07814418282788" required />
                            </div>
                            <div class="col-12 p-3">
                                <label for="inptAgrCodAct">Latitud</label>
                                <input type="text" class="form-control input-form col-12 text-center" title="Se requiere este campo" disabled runat="server" id="latitud" placeholder="9.929564942110702" required />
                            </div>
                            <div class="col-12 p-3">
                                <label for="inptAgrCodAct">Peso extra</label>
                                <input type="text" class="form-control input-form col-12 text-center" title="Se requiere este campo" runat="server" id="peso_extra" placeholder="500" required />
                            </div>
                            <div class="col-12 p-3 mt-5">
                                <button type="button" class="btn btn-consulta col-12" runat="server" id="Consultar" title="Agregar" onserverclick="Consultar_ServerClick">Agregar</button>
                            </div>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" class="col-6">
            <ContentTemplate>
                <div class="col-12 div-center">
                    <div id="map"></div>
                    <pre id="coordinates" class="coordinates"></pre>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="form-row col-12 mt-4 p-0">
                <div class="col-12 sub-content p-5">
                    <label for="inptAgrCodAct">El camión seleccionado para realizar la parada es el siguiente</label>
                    <textarea type="text" class="form-control input-form col-12 p-4" id="resultado" runat="server" disabled required />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




    <div class="col-12 mt-5 content">
        <asp:UpdatePanel runat="server" ID="UpdatePanelOperaciones" UpdateMode="Conditional">
            <ContentTemplate>
                </div>
                        <table class="table table-hover" id="TableOperaciones">
                            <thead class="text-center" style="background-color: black;">
                                <tr style="color: white!important">
                                    <th class="Roboto-Thin text-center"># Camión</th>
                                    <th class="Roboto-Thin text-center">Capacidad Total</th>
                                    <th class="Roboto-Thin text-center">Capacidad Utilizada</th>
                                    <th class="Roboto-Thin text-center">Punto Partida</th>
                                    <th class="Roboto-Thin text-center">Punto Final</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ClientIDMode="AutoID" ID="tb_list">
                                    <ItemTemplate>
                                        <tr class="text-center" runat="server" id="rowOperaciones">
                                            <td class="Roboto-Regular">
                                                <%# Eval("id") %>
                                            </td>
                                            <td class="Roboto-Regular">
                                                <%# Eval("capacidad_total") %>
                                            </td>
                                            <td class="Roboto-Regular">
                                                <%# Eval("capacidad_utilizada") %>
                                            </td>
                                            <td class="Roboto-Regular">
                                                <%# Eval("ruta_ini") %>
                                            </td>
                                            <td class="Roboto-Regular">
                                                <%# Eval("ruta_fin") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script>
        mapboxgl.accessToken = 'pk.eyJ1Ijoic3RldmVucnMiLCJhIjoiY2t2Mm9kY2hyM2wwMzJvcTlmMGkxcXppaiJ9.8cpG0laAgnjpfCOWRTHnWw';
        const coordinates = document.getElementById('coordinates');
        const map = new mapboxgl.Map({
            container: 'map',
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [-84.07814418282788, 9.929564942110702],
            zoom: 8
        });

        const marker = new mapboxgl.Marker({
            draggable: true
        })
            .setLngLat([-84.07814418282788, 9.929564942110702])
            .addTo(map);

        function onDragEnd() {
            const lngLat = marker.getLngLat();
            coordinates.style.display = 'block';
            coordinates.innerHTML = `Longitude: ${lngLat.lng}<br />Latitude: ${lngLat.lat}`;
            $("#MainContent_longitud").val(lngLat.lng);
            $("#MainContent_latitud").val(lngLat.lat);
        }

        marker.on('dragend', onDragEnd);
    </script>
    ssss
</asp:Content>

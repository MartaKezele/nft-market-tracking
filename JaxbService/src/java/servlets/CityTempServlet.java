package servlets;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.URL;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.client.XmlRpcClient;
import org.apache.xmlrpc.client.XmlRpcClientConfigImpl;

public class CityTempServlet extends HttpServlet {

    private static final String SERVER_URL = "http://localhost:8000";

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String responseMessage;
        try {
            // Get query param
            String cityName = request.getParameter("city");

            // Make XmlRpc client
            XmlRpcClientConfigImpl config = new XmlRpcClientConfigImpl();
            config.setServerURL(new URL(SERVER_URL));

            XmlRpcClient client = new XmlRpcClient();
            client.setConfig(config);

            // Call remote procedure
            Object[] params = new Object[]{cityName};
            responseMessage = (String) client.execute(
                    "Methods.getTemperatureForCity", params);

            // Write response
            response.setContentType("text;charset=UTF-8");
            try (PrintWriter out = response.getWriter()) {
                out.println(responseMessage);
            }
        } catch (XmlRpcException ex) {
            responseMessage = ex.getMessage();
            Logger.getLogger(CityTempServlet.class.getName()).log(Level.SEVERE,
                    null, ex);
        }
        response.setContentType("text;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {
            out.println(responseMessage);
        }
    }

}

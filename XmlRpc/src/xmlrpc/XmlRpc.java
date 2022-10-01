package xmlrpc;

import java.io.IOException;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServer;
import org.apache.xmlrpc.webserver.WebServer;

public class XmlRpc {

    public static void main(String[] args) throws IOException, XmlRpcException {
        System.out.println("Starting server...");
        WebServer server = new WebServer(8000);
        XmlRpcServer xmlServer = server.getXmlRpcServer();
        PropertyHandlerMapping phm = new PropertyHandlerMapping();
        phm.addHandler("Methods", Methods.class);
        xmlServer.setHandlerMapping(phm);
        
        server.start();
        System.out.println("Server started");
        System.out.println("Waiting for request...");
    }
    
}

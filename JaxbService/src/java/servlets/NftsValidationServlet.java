package servlets;

import generated.ArrayOfNft;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.xml.XMLConstants;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import org.xml.sax.SAXException;

public class NftsValidationServlet extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest request,
            HttpServletResponse response)
            throws ServletException, IOException {
        String xmlFile = "replace_with_path";
        String xsdFile = "replace_with_path";
        String responseMessage;
        
        try (InputStream inputStream = new FileInputStream(xmlFile)) {
            //Get JAXBContext
            JAXBContext jaxbContext = JAXBContext.newInstance(ArrayOfNft.class);

            //Create Unmarshaller
            Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();

            //Setup schema to validate against
            SchemaFactory sf = SchemaFactory.newInstance(
                    XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema nftsSchema = sf.newSchema(new File(xsdFile));
            jaxbUnmarshaller.setSchema(nftsSchema);

            //Unmarshal xml file
            jaxbUnmarshaller.unmarshal(inputStream);
            responseMessage = "Validation successful";
        } catch (JAXBException | SAXException ex) {
            responseMessage = "Validation failed with message: " + ex.getCause();
        }

        response.setContentType("text;charset=UTF-8");
        try (PrintWriter out = response.getWriter()) {
            out.println(responseMessage);
        }
    }

}

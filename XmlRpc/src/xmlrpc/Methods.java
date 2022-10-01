package xmlrpc;

import generated.GradType;
import generated.Hrvatska;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.XMLConstants;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import org.xml.sax.SAXException;

public class Methods {

    private static final String ENDPOINT = "https://vrijeme.hr/hrvatska_n.xml";

    public String getTemperatureForCity(String cityName) {
        String tempResp;
        try {
            URL url = new URL(ENDPOINT);
            HttpURLConnection con = (HttpURLConnection) url.openConnection();
            con.setRequestMethod("GET");
            InputStream inputStream = con.getInputStream();

            String xsdFile = "replace_with_your_path";
            JAXBContext jaxbContext;
            //Get JAXBContext
            jaxbContext = JAXBContext.newInstance(Hrvatska.class);

            //Create Unmarshaller
            Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();

            //Setup schema validator
            SchemaFactory sf = SchemaFactory.newInstance(
                    XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema nftsSchema = sf.newSchema(new File(xsdFile));
            jaxbUnmarshaller.setSchema(nftsSchema);

            //Unmarshal xml file
            Hrvatska responseObj = (Hrvatska) jaxbUnmarshaller.unmarshal(inputStream);
            List<Object> list = responseObj.getDatumTerminOrGrad();
            for (int i = 1; i < list.size(); i++) {
                GradType city = (GradType) list.get(i);
                if (city.getGradIme().toLowerCase().equals(cityName.toLowerCase())) {
                    tempResp = city.getPodatci().getTemp();
                    return tempResp;
                }
            }
            tempResp = "City with name '" + cityName + "' not found"; 
        } catch (IOException | JAXBException | SAXException ex) {
            Logger.getLogger(Methods.class.getName()).log(Level.SEVERE, null, ex);
            tempResp = ex.getMessage();
        }
        return tempResp;
    }

}

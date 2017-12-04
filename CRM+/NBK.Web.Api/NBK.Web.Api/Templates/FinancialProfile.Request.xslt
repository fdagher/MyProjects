<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:param name="token"/>
  <xsl:param name="customerNumber"/>
  <xsl:template match="/">
    <soap:Envelope xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
      <soap:Body>
        <BFX>
          <Header>
            <Version>1</Version>
            <ITData/>
            <Context>
              <NBKToken>
                <xsl:value-of select="$token"/>
              </NBKToken>
            </Context>
          </Header>
          <Message id="id_0" type="R" version="1">
            <TType>CustomerProcessService.Portfolio.GetCustomerProfile</TType>
            <GetCustomerProfileRq>
              <CustId>
                <CustPermId>
                  <xsl:value-of select="$customerNumber"/>
                </CustPermId>
              </CustId>
              <CustProfInqMode>Financial</CustProfInqMode>
            </GetCustomerProfileRq>
          </Message>
        </BFX>
      </soap:Body>
    </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>
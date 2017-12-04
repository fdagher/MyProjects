<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:param name="token"/>
  <xsl:param name="accountNumber"/>

  <xsl:template match="/">
    <soap:Envelope xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
      <soap:Body>
        <BFX xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
          <Header>
            <Version>1</Version>
            <ITData>
            </ITData>
            <Context>
              <NBKToken>
                <xsl:value-of select="$token"/>
              </NBKToken>
            </Context>
            <BatchOptions>
              <ProcessingMode>Parallel</ProcessingMode>
              <StopOnError>true</StopOnError>
            </BatchOptions>
            <QualityOfService />
          </Header>
          <Message id="1" type="R" version="1">
            <TType>ProductProcessService.DepAcct.GetDetails</TType>
            <GetDepositAccountDetailsRq>
              <BankProdId>
                <ProdId>
                  <xsl:value-of select="$accountNumber"/>
                </ProdId>
              </BankProdId>
              <DepAcctInquiryMode>Simple</DepAcctInquiryMode>
            </GetDepositAccountDetailsRq>
          </Message>
        </BFX>
      </soap:Body>
    </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>
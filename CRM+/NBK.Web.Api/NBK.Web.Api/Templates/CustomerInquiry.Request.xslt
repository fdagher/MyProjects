<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:param name="token"/>
  <xsl:param name="customerNumber"/>

  <xsl:template match="/">
    <soap:Envelope xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
      <soap:Body>
        <BFX>
          <Header>
            <Version>1</Version>
            <Context>
              <NBKToken>
                <xsl:value-of select="$token" />
              </NBKToken>
            </Context>
          </Header>
          <Message id="1" type="R" version="3">
            <TType>CustomerService.Customer.Inquiry</TType>
            <CustInqRq>
              <Filter>
                <Expression>
                  <LeftOperand>
                    <PropName>CustomerNumber</PropName>
                  </LeftOperand>
                  <Operator>
                    <Type>Comparison</Type>
                    <Val>Equal</Val>
                  </Operator>
                  <RightOperand>
                    <Literal>
                      <Type>string</Type>
                      <Val>
                        <xsl:value-of select="$customerNumber"/>
                      </Val>
                    </Literal>
                  </RightOperand>
                </Expression>
              </Filter>
              <InquireSimple>false</InquireSimple>
              <InquireBy>CustomerNumber</InquireBy>
            </CustInqRq>
          </Message>
        </BFX>
      </soap:Body>
    </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>
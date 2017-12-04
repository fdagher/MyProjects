<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
 xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" >

  <xsl:template match="/">
    <CustomerAlertResponse>
      <xsl:choose>
        <xsl:when test="//ResponseStatus">
          <ErrCode>
            <xsl:value-of select="//ResponseStatus/ResponseStatusCode/."/>
          </ErrCode>
          <ErrDesc>
            <xsl:value-of select="//ResponseStatus/ResponseStatusDescription/."/>
          </ErrDesc>
        </xsl:when>
        <xsl:otherwise>
          <ErrCode>0</ErrCode>
          <ErrDesc>OK</ErrDesc>
        </xsl:otherwise>
      </xsl:choose>

      <Alerts>
        <xsl:for-each select="soap:Envelope/soap:Body/BFX/Message/GetCustomerProfileRs/CustProfInfo/CustAlert">
          <Alert>
            <Type>
              <xsl:value-of select="./CustProfAlertType/."/>
            </Type>
            <Indicator>
              <xsl:value-of select="./AlertInd/."/>
            </Indicator>
          </Alert>
        </xsl:for-each>
      </Alerts>
    </CustomerAlertResponse>
  </xsl:template>
</xsl:stylesheet>
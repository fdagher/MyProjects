<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
 xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" >
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:template match="/">
    <FinancialProfileResponse>
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

      <FinancialProfile>
        <FinancialEntry>
          <Type>1</Type>
          <Name>TotalCashLimit</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalCashLimit/Amt"/>
          </Value>
        </FinancialEntry>
        <FinancialEntry>
          <Type>1</Type>
          <Name>TotalNonCashLimit</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalNonCashLimit/Amt"/>
          </Value>
        </FinancialEntry>
      <FinancialEntry>
        <Type>1</Type>
        <Name>TotalEarnedAssets</Name>
        <Value>
          <xsl:value-of select="//CustProfInfo/TotalEarnedAssets/Amt"/>
        </Value>
      </FinancialEntry>
       <FinancialEntry>
          <Type>1</Type>
          <Name>TotalFreeFunds</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalFreeFunds/Amt"/>
          </Value>
        </FinancialEntry>
        <FinancialEntry>
          <Type>1</Type>
          <Name>TotalNBKFunds</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalNBKFunds/Amt"/>
          </Value>
        </FinancialEntry>
        <FinancialEntry>
          <Type>2</Type>
          <Name>TotalCashLiability</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalCashLiability/Amt"/>
          </Value>
        </FinancialEntry>
        <FinancialEntry>
          <Type>2</Type>
          <Name>TotalIndirectLiability</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalIndirectLiability/Amt"/>
          </Value>
        </FinancialEntry>
        <FinancialEntry>
          <Type>2</Type>
          <Name>TotalNonCashLiability</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalNonCashLiability/Amt"/>
          </Value>
        </FinancialEntry>
        <FinancialEntry>
          <Type>2</Type>
          <Name>TotalCollateral</Name>
          <Value>
            <xsl:value-of select="//CustProfInfo/TotalCollateral/Amt"/>
          </Value>
        </FinancialEntry>
     </FinancialProfile>




      <TotalNBKFunds>
        <xsl:value-of select="//CustProfInfo/TotalNBKFunds/Amt"/>
      </TotalNBKFunds>

    </FinancialProfileResponse>
  </xsl:template>
</xsl:stylesheet>


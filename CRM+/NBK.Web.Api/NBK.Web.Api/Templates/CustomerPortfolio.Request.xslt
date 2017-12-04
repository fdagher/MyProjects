
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:param name="token"/>
  <xsl:param name="customerNumber"/>
  <xsl:param name="productListType"/>
  <xsl:param name="inquiryMode"/>
  <xsl:param name="productStatus"/>
  <xsl:param name="custLinkMode"/>

  <xsl:template match="/">
    <soap:Envelope xmlns:s="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
      <soap:Body>
        <BFX>
          <Header>
            <Version>1</Version>
            <Context>
              <NBKToken>
                <xsl:value-of select="$token"/>
              </NBKToken>
            </Context>
          </Header>
          <Message id="1" type="R" version="1" xmlns="">
            <TType>CustomerProcessService.Portfolio.GetCustomerPortfolio</TType>
            <GetCustomerPortfolioRq>
              <Filter>
                <Expression>
                  <LeftOperand>
                    <Expression>
                      <LeftOperand>
                        <Expression>
                          <LeftOperand>
                            <Expression>
                              <LeftOperand>
                                <PropName>CustLinkMode</PropName>
                              </LeftOperand>
                              <Operator>
                                <Type>Comparison</Type>
                                <Val>Equal</Val>
                              </Operator>
                              <RightOperand>
                                <Literal>
                                  <Type>string</Type>
                                  <Val>
                                    <xsl:value-of select="$custLinkMode"/>
                                  </Val>
                                </Literal>
                              </RightOperand>
                            </Expression>
                          </LeftOperand>
                          <Operator>
                            <Type>Logical</Type>
                            <Val>And</Val>
                          </Operator>
                          <RightOperand>
                            <Expression>
                              <LeftOperand>
                                <PropName>InquiryMode</PropName>
                              </LeftOperand>
                              <Operator>
                                <Type>Comparison</Type>
                                <Val>Equal</Val>
                              </Operator>
                              <RightOperand>
                                <Literal>
                                  <Type>string</Type>
                                  <Val>
                                    <xsl:value-of select="$inquiryMode"/>
                                  </Val>
                                </Literal>
                              </RightOperand>
                            </Expression>
                          </RightOperand>
                        </Expression>
                      </LeftOperand>
                      <Operator>
                        <Type>Logical</Type>
                        <Val>And</Val>
                      </Operator>
                      <RightOperand>
                        <Expression>
                          <LeftOperand>
                            <Expression>
                              <LeftOperand>
                                <PropName>ProdListType</PropName>
                              </LeftOperand>
                              <Operator>
                                <Type>Comparison</Type>
                                <Val>Equal</Val>
                              </Operator>
                              <RightOperand>
                                <Literal>
                                  <Type>string</Type>
                                  <Val>
                                    <xsl:value-of select="$productListType"/>
                                  </Val>
                                </Literal>
                              </RightOperand>
                            </Expression>
                          </LeftOperand>
                          <Operator>
                            <Type>Logical</Type>
                            <Val>And</Val>
                          </Operator>
                          <RightOperand>
                            <Expression>
                              <LeftOperand>
                                <PropName>ProductStatus</PropName>
                              </LeftOperand>
                              <Operator>
                                <Type>Comparison</Type>
                                <Val>Equal</Val>
                              </Operator>
                              <RightOperand>
                                <Literal>
                                  <Type>string</Type>
                                  <Val>
                                    <xsl:value-of select="$productStatus"/>
                                  </Val>
                                </Literal>
                              </RightOperand>
                            </Expression>
                          </RightOperand>
                        </Expression>
                      </RightOperand>
                    </Expression>
                  </LeftOperand>
                  <Operator>
                    <Type>Logical</Type>
                    <Val>And</Val>
                  </Operator>
                  <RightOperand>
                    <Expression>
                      <LeftOperand>
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
                      </LeftOperand>
                      <Operator>
                        <Type>Logical</Type>
                        <Val>And</Val>
                      </Operator>
                      <RightOperand>
                        <Expression>
                          <LeftOperand>
                            <PropName>IncludeTransferFlag</PropName>
                          </LeftOperand>
                          <Operator>
                            <Type>Logical</Type>
                            <Val>Equal</Val>
                          </Operator>
                          <RightOperand>
                            <Literal>
                              <Type>bolean</Type>
                              <Val>True</Val>
                            </Literal>
                          </RightOperand>
                        </Expression>
                      </RightOperand>
                    </Expression>
                  </RightOperand>
                </Expression>
              </Filter>
            </GetCustomerPortfolioRq>
          </Message>
        </BFX>
      </soap:Body>
    </soap:Envelope>
  </xsl:template>
</xsl:stylesheet>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DGFactory
{
    public class UIProductionLine : BaseUI
    {
        private ProductLine _currentLine;

        private Text _customerInfoText;
        private Text _productInfoText;
        private Text _paceText;
        private Text _productCountText;
        private Text _errorCountText;

        // Start is called before the first frame update
        void Start()
        {
            Transform content = transform.Find("ScrollView/Viewport/Content");

            _customerInfoText = content.Find("PanelInfoCustomer/textContent").GetComponent<Text>();
            _productInfoText = content.Find("PanelInfoProduct/textContent").GetComponent<Text>();
            _paceText = content.Find("PanelPace/textContent").GetComponent<Text>();
            _productCountText = content.Find("PanelInfoProductionCount/textContent").GetComponent<Text>();
            _errorCountText = content.Find("PanelInfoErrorCount/textContent").GetComponent<Text>();
        }


        public void Refresh(ProductLine line)
        {
            _currentLine = line;
        }

        // Update is called once per frame
        void Update()
        {
            if(_currentLine != null)
            {
                _customerInfoText.text = _currentLine.ClientName;
                _productInfoText.text = _currentLine.CurrentProduct.ProdutName;
                _paceText.text = _currentLine.Pace.ToString();
                _productCountText.text = _currentLine.ProductionCount.ToString();
                _errorCountText.text = _currentLine.BadProductionCount.ToString();
            }
        }
    }
}


//import { getJson } from ''
import echarts from 'echarts'
class MyEcharts {
  constructor(echartId,options,callBackObj) {
    this.echartId = echartId;

    this.callBackObj = callBackObj;
    this.options=options;
  }

  initEcharts() {
    let callBackObj = this.callBackObj;
    let container = document.getElementById(this.echartId);
    let myChart = echarts.init(container);
    let options =this.options;
    myChart.setOption(options);
    //getJson(this.url, this.param).then(response => {
    //  myChart.setOption(chartOptionsTemplate.histogram_one);
    //});
    //自适应高度和宽度
    window.addEventListener("resize", () => { myChart.resize(); });
    //点击事件click
    myChart.on("click", function (param) {
      if ($.isPlainObject(callBackObj)) {
        if (callBackObj.click != undefined) {
          callBackObj.click(param);
        }
      }
    });
  }

}

export default MyEcharts

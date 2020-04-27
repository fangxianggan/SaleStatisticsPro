<template>
  <div class="dashboard-editor-container">
    <panel-group @handleSetLineChartData="handleSetLineChartData" />

    <div id="aaa" class="chart" style="width:100%;height:600px;"></div>
  </div>
</template>
<script>
import chartOptionsTemplate from "@/echarttemplate/chart";
import PanelGroup from "./components/PanelGroup";
import myEcharts from "@/utils/echarts";

const lineChartData = {
  newVisitis: {
    expectedData: [100, 120, 161, 134, 105, 160, 165],
    actualData: [120, 82, 91, 154, 162, 140, 145]
  },
  messages: {
    expectedData: [200, 192, 120, 144, 160, 130, 140],
    actualData: [180, 160, 151, 106, 145, 150, 130]
  },
  purchases: {
    expectedData: [80, 100, 121, 104, 105, 90, 100],
    actualData: [120, 90, 100, 138, 142, 130, 130]
  },
  shoppings: {
    expectedData: [130, 140, 141, 142, 145, 150, 160],
    actualData: [120, 82, 91, 154, 162, 140, 130]
  }
};
export default {
  name: "Dashboard",
  components: {
    PanelGroup
  },
  computed: {},
  data() {
    return {
      lineChartData: lineChartData.newVisitis
    };
  },
  created() {},
  mounted() {
    this.drawEchart();
  },
  methods: {
    handleSetLineChartData(type) {
      this.lineChartData = lineChartData[type];
    },
    drawEchart() {
      var url="/Statistics/GetBarChart";
      var data={};
      this.$ajax(url, data).then(response => {
        //console.log(response.data);
       
        chartOptionsTemplate.histogram_one.legend.data=["产品库存量"];
        chartOptionsTemplate.histogram_one.xAxis[0].data=response.data.map(function(x){
         return x.productName;
        });
         chartOptionsTemplate.histogram_one.series[0].data=response.data.map(function(x){
         return x.stock;
        });
        chartOptionsTemplate.histogram_one.series[0].name="库存";
        console.log(chartOptionsTemplate.histogram_one);
        var echart = new myEcharts("aaa", chartOptionsTemplate.histogram_one, {
          click: function(param) {
            console.log(param);
          }
        });
        echart.initEcharts();
      });
    }
  }
};
</script>
<style lang="scss" scoped>
.dashboard {
  &-container {
    margin: 30px;
  }

  &-text {
    font-size: 30px;
    line-height: 46px;
  }
}
</style>

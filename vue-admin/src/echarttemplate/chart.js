
import myAction from '@/utils/baseutil'

var color = ['#2a84e9', '#ff8a76', '#8426ff', '#8d4654', '#745af2', '#1e88e5', '#26c6da', '#eceff1', '#ff3c55', '#3c8dbc', '#434348', '#eeaaee', '#2b908f', '#8d4654'];

//面积图 区域的透明度
var baseAreaStyle_opacity = 0.06;

var base_xAxis = {
  axisLabel: { //调整x轴的lable
    interval: 0,//文字倾斜
    rotate: 20,//文字倾斜
    textStyle: {
      fontSize: 0, // 让字体变大
      color: '#888'
    },
    lineStyle: {
    //  color: '#000',
      width: 1
    },
    //坐标轴刻度标签的相关设置。
    formatter: function (params) {
      var newParamsName = "";// 最终拼接成的字符串
      var paramsNameNumber = params.length;// 实际标签的个数
      var provideNumber = 6;// 每行能显示的字的个数
      var rowNumber = Math.ceil(paramsNameNumber / provideNumber);// 换行的话，需要显示几行，向上取整
      /**
       * 判断标签的个数是否大于规定的个数， 如果大于，则进行换行处理 如果不大于，即等于或小于，就返回原标签
       */
      // 条件等同于rowNumber>1
      if (paramsNameNumber > provideNumber) {
        /** 循环每一行,p表示行 */
        for (var p = 0; p < rowNumber; p++) {
          var tempStr = "";// 表示每一次截取的字符串
          var start = p * provideNumber;// 开始截取的位置
          var end = start + provideNumber;// 结束截取的位置
          // 此处特殊处理最后一行的索引值
          if (p == rowNumber - 1) {
            // 最后一次不换行
            tempStr = params.substring(start, paramsNameNumber);
          } else {
            // 每一次拼接字符串并换行
            tempStr = params.substring(start, end) + "\n";
          }
          newParamsName += tempStr;// 最终拼成的字符串
        }

      } else {
        // 将旧标签的值赋给新标签
        newParamsName = params;
      }
      //将最终的字符串返回
      return newParamsName
    }


  },
  splitLine: {
    show: true,
    lineStyle: {
      color: 'rgba(255,255,255,0.05)',
      type: 'solid'
    }
  },
  axisLine: { //调整x轴的lable
    show: true,
    lineStyle: {
      color: 'rgba(155,155,155,0.50)',
      width: 1,
      type: 'solid'
    }
  },
};
var base_yAxis =
{
  axisLabel: { //调整x轴的lable
    //interval: interval,
    rotate: 0,
    textStyle: {
      fontSize: 0, // 让字体变大
      color: '#888'
    },
    lineStyle: {
    //  color: '#000',
      width: 1
    }

  },
  splitLine: {
    show: true,
    lineStyle: {
      color: 'rgba(255,255,255,0.05)',
      type: 'solid'
    }
  },
  axisLine: { //调整x轴的lable
    show: true,
    lineStyle: {
      color: 'rgba(155,155,155,0.50)',
      width: 1,
      type: 'solid'
    }
  }

};
var base_tooltip = {
  trigger: 'axis',
  formatter: function (params) {
    var res = "";
    var arr = [];
    if ($.isArray(params)) {
      res += params[0].name + "</br>";
      arr = params;
    } else {
      res += params.name + "</br>";
      arr.push(params);
    }
    // console.log(params);
    $.each(arr, function (i, e) {
      //  console.log(e);
      switch (e.seriesType) {
        case "line":
          if (e.seriesName.indexOf("同比") > -1 || e.seriesName.indexOf("比") > -1) {
            res += e.seriesName + "：" + e.value + "%</br>";
          } else {
            res += e.seriesName + "：" + e.value + "</br>";
          }
          break;

        default:
          res += e.seriesName + "：" + myAction.formattingNumbers(e.value, e.seriesName) + "</br>";
          break;
      }
    });
    return res;
  }
};

var base_lengend = {
  textStyle: {
  // color: 'rgba(255,255,255,.8)'
  },
  top: 10,
  left: 'center'
};

var base_dataZoom = {
  type: 'slider',
  xAxisIndex: 0,
  show: true,
}
var base_grid =
{
  left: "10%",
  right: "5%",
  height: "auto",
  width: "auto",
  bottom: 100
}

let chartOptionsTemplate = {
  //环图
  pie_progress: {
    title: {
      text: "药占比",
      subtext: '同期：32%\n\n同比：26%\n\n上期：32%\n\n环比：26%',
      top: '25%',
      textStyle: {
        color: 'rgba(255,255,255,.8)',
        fontWeight: 'normal',
        fontFamil: 'Microsoft YaHei'
      }
    },
    series: [
      {
        type: 'pie',
        clockWise: false,
        radius: ["60%", "70%"],
        itemStyle: {
          normal: {
            color: '#389af4',
            shadowColor: '#389af4',
            shadowBlur: 0,
            label: {
              show: false
            },
            labelLine: {
              show: false
            },
          }
        },
        hoverAnimation: false,
        center: ['60%', '50%'],
        data: [{
          value: 88,
          label: {
            normal: {
              formatter: function (params) {
                return params.value + '%';
              },
              position: 'center',
              show: true,
            }
          },
        }, {
          value: 100 - 88,
          name: 'invisible',
          itemStyle: {
            normal: {
              color: 'rgba(255,255,255,.35)'
            },
            emphasis: {
              color: 'rgba(255,255,255,.35)'
            }
          }
        }]
      }
    ]
  },
  //饼图
  pie:
  {
    color: color,
    tooltip: $.extend(true, {}, base_tooltip, { trigger: 'item', }),
    legend: $.extend(true, {}, base_lengend, {
      data: ["心内科", "儿科", "骨科", "生殖科"],
    }),
    series: [{
      name: "门急诊人次本期",
      type: 'pie',
      radius: '100%',
      roseType: 'radius',
      data: [
        { value: 186944, name: '心内科' },
        { value: 272157, name: '儿科' },
        { value: 163567, name: '骨科' },
        { value: 233330, name: '生殖科' }
      ],
      itemStyle: {
        normal: {
          label: {
            show: true,
            formatter: '{b}:{c}({d} %)'
          },
          labelLine: {
            show: true,
            length: 0.1,
            length2: 0.1
          }
        }
      }

    }],

  },
  //散点图
  scatter: {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["科室", "均次同比上升"],

    }),
    tooltip: $.extend(true, {}, base_tooltip),
    //tooltip: {
    //    trigger: 'item',
    //    formatter: function (params) {
    //        var $html = "";
    //        if (params.componentType == "series") {
    //            $html = params.value[4] + '<br/>'
    //                + "均次费用:" + params.value[1] + '&nbsp;&nbsp;&nbsp; 同比:' + params.value[3] + "%" + "<br/>"
    //                + "门急诊人次:" + params.value[0] + '&nbsp;&nbsp;&nbsp; 同比:' + params.value[2] + "%" + "<br/>";
    //        }
    //        return $html;
    //    }
    //},
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value'
    })],
    xAxis: [$.extend(true, {}, base_xAxis, {
      type: 'value'
    })],
    series: [
      {
        "data": [
          [12222, 23453, 0.23, -1.23, '心内科'],
          [15222, 13453, 0.33, -1.83, '儿科'],
          [18222, 53453, 0.13, -0.3, '骨科'],
        ],
        "name": "科室",
        "type": 'scatter',
        markLine: {
          data: [{
            name: '平均线',
            // 支持 'average', 'min', 'max'
            type: 'average',
            itemStyle: {
              normal: {
                color: 'rgba(255,0,0,.6)'//平均值线的颜色
              }
            }

          }]
        }
      },
      {
        "data": [[12722, 12453, 0.83, 1.53, '生殖科']],
        "name": "均次同比上升",
        "type": 'scatter'
      },

    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //基础散点图
  scatter_base: {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次", "急诊人次", "住院人次"],

    }),
    tooltip: $.extend(true, {}, base_tooltip),
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value'
    })],
    xAxis: [$.extend(true, {}, base_xAxis, {
      type: 'value'
    })],
    series: [
      {
        "data": [
          [12222, 23453, '心内科'],
          [15222, 13453, '儿科'],
          [18222, 53453, '骨科'],
          [12722, 12453, '生殖科']
        ],
        "name": "门急诊人次",
        "type": 'scatter',
        markLine: {
          data: [{
            name: '平均线',
            // 支持 'average', 'min', 'max'
            type: 'average',
            itemStyle: {
              normal: {
                color: 'rgba(255,0,0,.6)'//平均值线的颜色
              }
            }

          }]
        }
      },
      {
        "data": [
          [12222, 13453, '心内科'],
          [15222, 11453, '儿科'],
          [18222, 13453, '骨科'],
          [12522, 12453, '生殖科']
        ],
        "name": "急诊人次",
        "type": 'scatter'
      },
      {
        "data": [
          [13222, 13453, '心内科'],
          [15222, 16653, '儿科'],
          [18222, 53453, '骨科'],
          [12722, 12453, '生殖科']
          [12522, 12403, '生殖科']
        ],
        "name": "住院人次",
        "type": 'scatter'
      },

    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //矩阵图
  matrix: {
    //title: {
    //    text: '门急诊人次本期',
    //    left: 'center',
    //    top: 10
    //},
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期"],
    }),
    color: color,
    tooltip: $.extend(true, {}, base_tooltip, { trigger: 'item' }),
    series: [
      {
        name: '门急诊人次本期',
        type: 'treemap',
        itemStyle: {
          normal: {
            label: {
              show: true,
              formatter: "{b}"
            },
            borderWidth: 1
          },
          emphasis: {
            label: {
              show: true
            }
          }
        },
        data: [
          { value: 1886944, name: '心内科' },
          { value: 272157, name: '儿科' },
          { value: 63567, name: '骨科' },
          { value: 33330, name: '生殖科' }
        ]
      }
    ]
  },
  //水滴图
  waterdrop: {
    color: color,
    legend: $.extend(true, {}, base_lengend),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis,
      {
        axisTick: { show: false },
        axisLine: { show: false },
        data: ["心内科", "儿科", "骨科", "生殖科"],

      }
    )],
    yAxis: [$.extend(true, {}, base_yAxis,
      {
        type: 'value',
      })
    ],
    series: [
      {
        name: "门急诊人次本期",
        type: 'pictorialBar',
        barCategoryGap: '-130%',
        symbol: 'path://M0,10 L10,10 C5.5,10 5.5,5 5,0 C4.5,5 4.5,10 0,10 z',
        itemStyle: {
          normal: {
            opacity: 0.5
          },
          emphasis: {
            opacity: 1
          }
        },
        data: [{
          value: 23,
          symbol: 'path://M0,10 L10,10 C5.5,10 5.5,5 5,0 C4.5,5 4.5,10 0,10 z' // 只对此数据项生效
        }, {
          value: 56,
          symbol: 'path://M0,10 L10,10 C5.5,10 5.5,5 5,0 C4.5,5 4.5,10 0,10 z' // 只对此数据项生效
        }, {
          value: 23,
          symbol: 'path://M0,10 L10,10 C5.5,10 5.5,5 5,0 C4.5,5 4.5,10 0,10 z' // 只对此数据项生效
        }, {
          value: 56,
          symbol: 'path://M0,10 L10,10 C5.5,10 5.5,5 5,0 C4.5,5 4.5,10 0,10 z' // 只对此数据项生效
        }],
        //data: [
        //    186944,
        //    272157,
        //    163567,
        //    233330
        //],
        z: 10,
        label: {
          normal: {
            show: true,
            position: 'top',
            formatter: function (params) {
              return params['value'];
            }
          }
        }
      }
    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //柱状图 (一柱)
  histogram_one:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"],

    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',

    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'bar',
      name: '门急诊人次本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //柱状图(2柱)
  histogram_two:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次上期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"],
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'bar',
      name: '门急诊人次本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'bar',
      name: '门急诊人次同期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图(一线)
  broken_oneline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',

    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: '门急诊人次本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图(二线)
  broken_twoline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次上期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: '门急诊人次本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: '门急诊人次同期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（三线）
  broken_threeline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（四线）
  broken_fourline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（五线）
  broken_fiveline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    //图表的位置设置
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（六线）
  broken_sixline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期", "f本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'f本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（七线）
  broken_sevenline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期", "f本期", "g本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'f本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'g本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（八线）
  broken_eightline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期", "f本期", "g本期", "h本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'f本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'g本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'h本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（九线）
  broken_nineline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期", "f上期", "g本期", "h本期", "i本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'f同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'g本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'h本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'i本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //折线图（十线）
  broken_tenline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期", "f本期", "g上期", "h本期", "i本期", "j本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'f本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'g同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'h本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'i本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'j本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //曲线图(一线)
  song_oneline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',

    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: '门急诊人次本期',
      smooth: true
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //曲线图(二线)
  song_twoline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次上期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: '门急诊人次本期',
      smooth: true
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: '门急诊人次同期',
      smooth: true
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //曲线图（三线）
  song_threeline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期',
      smooth: true
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期',
      smooth: true
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期',
      smooth: true
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //曲线图（四线）
  song_fourline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期',
      smooth: true
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期',
      smooth: true
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期',
      smooth: true
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期',
      smooth: true
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //曲线图（五线）
  song_fiveline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    //图表的位置设置
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
    series: [{
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期',
      smooth: true
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //曲线图（五线）
  song_sixline:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a本期", "b上期", "c本期", "d本期", "e本期", "f本期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    //图表的位置设置
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
    series: [{
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'a本期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'b同期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'c本期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'd本期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期',
      smooth: true
    }, {
      "data": myAction.setRandomNumberArr(),
      type: 'line',
      name: 'e本期',
      smooth: true
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },

  //面积图
  area_one:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": '门急诊人次本期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },

  //面积图
  area_two:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a期", "b期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'a期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'b期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //面积图
  area_three:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a期", "b期", "c期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'a期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'b期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'c期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //面积图
  area_four:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a期", "b期", "c期", "d期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'a期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'b期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'c期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'd期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //面积图
  area_five:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["a期", "b期", "c期", "d期", "e期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    series: [{
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'a期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'b期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'c期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'd期'
    }, {
      "data": myAction.setRandomNumberArr(),
      "type": 'line',
      "smooth": true,
      "areaStyle": { opacity: baseAreaStyle_opacity },
      "name": 'e期'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },

  //级地图
  polar:
  {
    color: color,
    chart: {
      backgroundColor: 'transparent',
      polar: true,
      events: {
        click: function (e) {

        }
      }
    },
    legend: {
      itemHoverStyle: { color: 'rgba(255,255,255,.8)' },
      itemStyle: { color: 'rgba(255,255,255,.8)' }, // 背景颜色
      verticalAlign: 'top',    //程度标的目标地位

    },
    title: [{ text: "门急诊人次" }],
    tooltip: {
      pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%</b> ({point.y:,.0f})<br/>',
      shared: true
    },
    xAxis: [{
      categories: ["心内科", "儿科", "骨科", "生殖科"]
    }],
    yAxis: [
      {
        enabled: false,
        labels: {
          enabled: false
        }
      }, {
        enabled: false,
        labels: {
          enabled: false
        }
      }, {
        enabled: false,
        labels: {
          enabled: false
        }
      }],
    series: [{
      type: 'column',
      name: '门急诊人次本期',
      data: myAction.setRandomNumberArr(),
      pointPlacement: 'on'
    }, {
      type: 'line',
      name: '门急诊人次同期',
      data: myAction.setRandomNumberArr(),
      pointPlacement: 'on'
    }, {
      type: 'area',
      name: '门急诊人次同比',
      pointPlacement: 'on',
      data: myAction.setRandomNumberArr(),
    }],
    credits: {
      enabled: false
    },
    plotOptions: {
      series: {
        cursor: 'pointer',
        events: {
          click: function (e) {

          }
        }
      },
      column: {
        pointPadding: 0,
        groupPadding: 0
      }
    },
  },
  //横一柱一线
  bar_x:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次同期"]
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      type: 'value'
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    series: [
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同期",
        "type": 'line'
      }
    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //横二柱
  bar_x_two:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次同期"]
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      type: 'value'
    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    series: [
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同期",
        "type": 'bar'
      }
    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //一柱一线
  oneBarLine:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次同期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value'
    })],
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    series: [
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同期",
        "type": 'line'
      }
    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //两柱两线
  twoBarLine: {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次同期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value'
    })],
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    series: [
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同期",
        "type": 'line'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "急诊人次同期",
        "type": 'line'
      }
    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //两柱一点
  twoBar_onePint: {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次同期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value'
    })],
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    series: [
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同比",
        "type": 'scatter'
      }

    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //两柱一线
  twoBar_oneLine: {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次同期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value'
    }), $.extend(true, {}, base_yAxis, {
      type: 'value'
    })],
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    series: [
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同比",
        "yAxisIndex": 1,
        "type": 'line'
      }

    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //三柱三线
  threeBarLine:
  {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["住院人次本期", "门急诊人次本期", "门急诊人次同期"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value'
    })],
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"]
    })],
    series: [
      {
        data: myAction.setRandomNumberArr(),
        "name": "住院人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "急诊人次本期",
        "type": 'bar'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "住院人次同比",
        "type": 'line'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "门急诊人次同比",
        "type": 'line'
      },
      {
        data: myAction.setRandomNumberArr(),
        "name": "急诊人次同比",
        "type": 'line'
      }
    ],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  },
  //堆叠图(4柱)
  stacking_four: {
    color: color,
    legend: $.extend(true, {}, base_lengend, {
      data: ["门急诊人次本期", "门急诊人次上期", "门急诊人次同期", "门急诊人次同比"],
    }),
    tooltip: $.extend(true, {}, base_tooltip),
    xAxis: [$.extend(true, {}, base_xAxis, {
      data: ["心内科", "儿科", "骨科", "生殖科"],

    })],
    yAxis: [$.extend(true, {}, base_yAxis, {
      type: 'value',
    })],
    grid: [{
      left: '30%',//因旋转导致名字太长的类目造成遮蔽，可以配合这两个属性
      bottom: '10%'// 分别表示：距离左边距和底部的距离，具体数值按实际情况调整
    }],
    series: [{
      data: myAction.setRandomNumberArr(),
      type: 'bar',
      stack: 'one',
      name: '门急诊人次本期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'bar',
      stack: 'one',
      name: '门急诊人次上期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'bar',
      stack: 'one',
      name: '门急诊人次同期'
    }, {
      data: myAction.setRandomNumberArr(),
      type: 'bar',
      stack: 'one',
      name: '门急诊人次同比'
    }],
    grid: $.extend(true, {}, base_grid),
    dataZoom: [$.extend(true, {}, base_dataZoom)],
  }

}

export default chartOptionsTemplate 

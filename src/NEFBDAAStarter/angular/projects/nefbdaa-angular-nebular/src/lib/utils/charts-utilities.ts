export interface ChartData {
  x: any;
  y: any;
}


export function mapToChartData(obj: any, xProperty: string, yProperty: string): ChartData {
  return {x: obj[xProperty], y: obj[yProperty]};
}


export const defaultTimeChartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  scales: {
    xAxes: [prepareTimeChartXAxes()],
    yAxes: [
      {
        display: true,
        scaleLabel: {
          display: true,
          labelString: 'Value',
        },
        gridLines: {
          display: true,
        },
        ticks: {
          beginAtZero: true,
        },
      },
    ],
  },
};


export const defaultBarChartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  barValueSpacing: 20,
  scales: {
    xAxes: [prepareTimeChartXAxes()],
    yAxes: [{
      ticks: {
        min: 0,
      },
      display: true,
      scaleLabel: {
        display: true,
        labelString: 'Value',
      },
      gridLines: {
        display: true,
      },
    }],
  },
};

export function prepareTimeChartXAxes(displayUnit: string = 'hour') {
  return {
    type: 'time',
    distribution: 'series',
    ticks: {
      source: 'data',
      autoSkip: true,
    },
    display: true,
    scaleLabel: {
      display: true,
      labelString: 'Date',
    },
    time: {
      unit: displayUnit,
      displayFormats: {
        hour: 'DD/MM/YY HH:mm',
        day: 'DD/MM/YY',
      },
    },
    legend: {
      display: true,
    },
    tooltips: {
      mode: 'label',
    },
  };
}
export function prepareLineDataset(title: string, color: string, data: Array<any>, datasetId: string = null) {
  const dataset = {
    label: title,
    data: data,
    backgroundColor: color,
    borderColor: color,
    pointRadius: 1,
    lineTension: 0,
    borderWidth: 2,
    fill: false,
    yAxisID: datasetId,
  };

  return dataset;
}


export const defaultInverterChartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  scales: {
    xAxes: [prepareTimeChartXAxes()],
    yAxes: [
      {
        type: 'linear',
        display: true,
        position: 'left',
        id: 'inverter',
        scaleLabel: {
          display: true,
          labelString: 'Value',
        },
        gridLines: {
          display: true,
        },
        ticks: {
          beginAtZero: true,
          max: 500,
          min: 0,
        },
      },
      {
        type: 'linear',
        display: true,
        position: 'right',
        id: 'pv',
        scaleLabel: {
          display: true,
          labelString: 'Value',
        },
        gridLines: {
          display: true,
        },
        ticks: {
          beginAtZero: true,
          max: 200,
          min: 0,
        },
      },
    ],
  },
  legend: {
    display: true,
  },
  tooltips: {
    mode: 'label',
  },

};
export const defaultVoltageChartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  scales: {
    xAxes: [prepareTimeChartXAxes()],
    yAxes: [
      {
        type: 'linear',
        display: true,
        position: 'left',
        id: 'voltage',
        scaleLabel: {
          display: true,
          labelString: 'Value',
        },
        gridLines: {
          display: true,
        },
        ticks: {
          beginAtZero: true,
          max: 250,
          min: 0,
        },
      },
      {
        type: 'linear',
        display: true,
        position: 'right',
        id: 'charge',
        scaleLabel: {
          display: true,
          labelString: 'Value',
        },
        gridLines: {
          display: true,
        },
        ticks: {
          beginAtZero: true,
          max: 120,
          min: 0,
        },
      },
    ],
  },
  legend: {
    display: true,
  },
  tooltips: {
    mode: 'label',
  },
};
export const defaultEnergyChartOptions = {
  responsive: true,
  maintainAspectRatio: false,
  barValueSpacing: 20,
  scales: {
    xAxes: [prepareTimeChartXAxes('day')],
    yAxes: [
      {
        type: 'linear',
        display: true,
        position: 'left',
        scaleLabel: {
          display: true,
          labelString: 'kWh',
        },
        gridLines: {
          display: true,
        },
        ticks: {
          beginAtZero: true,
          max: 100,
          min: 0,
        },
      },
    ],
  },
  legend: {
    display: true,
  },
  tooltips: {
    mode: 'label',
  },
};

const { merge } = require('webpack-merge');
const Dotenv = require('dotenv-webpack');

const commonConfig = require('./webpack.common.js');

module.exports = merge(commonConfig, {
  output: {
    publicPath: '/'
  },
  mode: 'production',
  devtool: 'source-map',
  plugins: [
    new Dotenv({
      systemvars: true,
      path: './env/.env.stage'
    })
  ]
});

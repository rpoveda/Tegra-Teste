/* eslint-disable */
import React, {Component} from 'react';
import DatePicker from "react-datepicker";
import Select from 'react-select';
import "react-datepicker/dist/react-datepicker.css";
import axios from 'axios';
import {Modal, Button, Alert} from 'react-bootstrap';
import SimpleReactValidator from 'simple-react-validator';
import { red } from 'color-name';


export default class BuscaVoo extends Component {
    constructor(props){
        super(props);

        this.state = {
            aeroportos : [],
            voos: [],
            origem: null,
            destino: null,
            data: new Date(),
            open: false,
            trechos: [],
            messageErro: null,
            disableBtn: false
        }

        this.validator = new SimpleReactValidator();
    }

    async componentDidMount()
    {
        axios.get("http://localhost:5000/aeroporto")
        .then(res => {
            this.setState({aeroportos : res.data});
        })
        .catch(err => {
            this.setState({messageErro : err.toString()});
        })
    }

    handleOrigem = value => {
        this.setState({origem : value});
    }

    handleDestino = value => {
        this.setState({destino : value})
    }

    handleData = value => {
        this.setState({data : value})
    }

    handleBusca = event => {
        event.preventDefault();
        if (!this.validator.allValid()) {
            this.validator.showMessages();
            this.forceUpdate();
            return;
          } 

        let _data = this.state.data;
        let _mes = (_data.getMonth() + 1).toString().length == 1 ? '0' + (_data.getMonth() + 1) : (_data.getMonth() + 1);
        let _dia = _data.getDate().toString().length == 1 ? '0' +  _data.getDate() : _data.getDate();
        let _dataBusca = `${_data.getFullYear()}-${_mes}-${_dia}`;
        let busca = {
            de: this.state.origem.codigo,
            para: this.state.destino.codigo,
            data: _dataBusca
        };

        this.setState({disableBtn:true})
        axios.post("http://localhost:5000/voo", busca)
        .then(resp => {
            this.setState({voos : resp.data, disableBtn: false});
        }).catch(err => {
            this.setState({messageErro : err.toString()});
        })
    }

    showTreco = (item, index) => {
        this.setState({trechos : item.trechos, open: true})
    }

    render(){
        return (
            <div>
                <div className="jumbotron">
                {
                    this.state.messageErro
                    &&
                    <Alert variant="danger" dismissible>
                        <Alert.Heading>Erro</Alert.Heading>
                        <p>
                            {this.state.messageErro}
                        </p>
                    </Alert>
                }
                <h1>Busca de Voô</h1>
                <form>
                    <div className="form-group">
                        <label>Origem</label>
                        <Select
                            getOptionValue={option => option["codigo"]}
                            getOptionLabel={option => `${option["codigo"]} - ${option["nome"]}`}
                            options={this.state.aeroportos}
                            placeholder="Origem"
                            value={this.state.origem}
                            onChange={this.handleOrigem}
                        />
                        <p style={{color: red}}>
                        {
                            this.validator.message('origem', this.state.origem, 'required', {default: "bla bla "})
                        }</p>
                    </div>
                    <div className="form-group">
                        <label>Destino</label>
                        <Select 
                            getOptionValue={option => option["codigo"]}
                            getOptionLabel={option => `${option["codigo"]} - ${option["nome"]}`}
                            options={this.state.aeroportos}
                            placeholder="Destino"
                            value={this.state.destino}
                            onChange={this.handleDestino}
                        />
                        <p style={{color: red}}>
                        {
                            this.validator.message('destino', this.state.destino, 'required', {default: "Vamos la"})
                        }</p>
                    </div>
                    <div className="form-group">
                        <label>Data</label><br />
                        <DatePicker 
                            dateFormat="dd/MM/yyyy"
                            className="form-control" 
                            selected={this.state.data}
                            onChange={this.handleData}
                        />

                        <p style={{color: red}}>
                        {
                            this.validator.message('data', this.state.data, 'required', {default: "bla bla "})
                        }</p>
                    </div>
                    <div className="form-group">
                        <button className="btn btn-primary" disabled={this.state.disableBtn} style={{float: 'right'}} onClick={this.handleBusca}>Buscar</button>
                    </div>
                </form>
                </div>
                <div className="panel-group">
                        <div className="panel panel-default">
                        <div className="panel-body">
                            <table className="table table-striped">
                                <thead>
                                    <tr>
                                        <th>Origem</th>
                                        <th>Destino</th>
                                        <th>Data</th>
                                        <th>Total</th>
                                        <th>Rota</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.state.voos.map((item,index) => {
                                        return(
                                        <tr>
                                            <td>{item.origem}</td>
                                            <td>{item.destino}</td>
                                            <td>{item.data}</td>
                                            <td>{item.total}</td>
                                            <td><a href='#' className="btn btn-primary" onClick={e => {this.showTreco(item, index)}}>Verificar Rota</a></td>
                                        </tr>
                                        )
                                    })}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>


                <Modal size="lg" show={this.state.open} onHide={e => { this.setState({open:false}) }}>
                    <Modal.Header closeButton>
                    <Modal.Title>Rotas</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <table className="table table-striped">
                            <thead>
                                <tr>
                                    <th>Origem</th>
                                    <th>Destino</th>
                                    <th>Hora Saida</th>
                                    <th>Hora Chegada</th>
                                    <th>Preco</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.trechos.map(item => {
                                    return(
                                        <tr>
                                            <td>{item.origem}</td>
                                            <td>{item.destino}</td>
                                            <td>{item.horaSaida}</td>
                                            <td>{item.horaChegada}</td>
                                            <td>{item.preco}</td>
                                        </tr>
                                    )
                                })}
                            </tbody>
                        </table>
                    </Modal.Body>
                    <Modal.Footer>
                    <Button variant="secondary" onClick={e => { this.setState({open:false}) }}>
                        Fechar
                    </Button>
                    </Modal.Footer>
                </Modal>                
            </div>
        )
    }
}
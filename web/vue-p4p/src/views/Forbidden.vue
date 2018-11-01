<template>
    <div class="backgroundp4p">
        <nav-layout>
            <b-container fluid class="container mt-4">
              <b-row class="justify-content-md-center">
                <b-col class="col-md-4">
                <div class="codeError">            
                    <span > 403 </span>
                </div>
                <div class ="frontPosition mt-5"> 
                        <span class="title"> Forbidden</span>        
                        <span class="text"> You don't have permission to access this page.</span>
                </div>
                </b-col>
              </b-row>
            </b-container>
        </nav-layout>
    </div>
</template>

<script >
    export default {
        name: 'Forbidden',
         mounted() {
       
        },
    }
</script>
<style scoped>
    .frontPosition{
        position: absolute;
        z-index: 800;
        float: left;
        height: 108px;
    }
    .codeError{
            position: absolute;
            z-index: 200;
            font-size: 140px;
            color:#e4e4e4a8;
            font-weight: bold;            
    }
     .title{
        font-size: 23px;
        color: #636262;
        float: left;
        position: absolute;
        bottom: 15px;
        left: 0;      
    }
    .text{
        font-size: 14px;
        color: #949494;
        float: left;
        position: absolute;
        bottom: 0;
        left: 0;
        width: 294px;        
    }
    .position-text
    {
        top: 40%;
        left: 50%;
        margin-right: -50%;
        transform: translate(-50%, -50%);
    }
</style>

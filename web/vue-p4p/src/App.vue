<template>
  <div id="app">
    <router-view :key="$route.path"/>
  </div>
</template>
<script>

export default {
  data: () => ({
    currentTemplate: "nav-layout",

  }),

    beforeUpdate() {
    this.$store.dispatch("loginModule/setAuthenticationStore");
    // this.$emit('page-change', this.$route.path)
  },
  mounted() {
    this.$store.dispatch("loginModule/setAuthenticationStore");
  }
};
</script>
<style>
html
{
  background-color: #cccccc;
}

.backgroundp4p
{
  background-color: #cccccc;
}
.hearder-card
{
   background-color:#bbbbbb !important;
   color: #ffffff;
   font-weight: bold;
}
.button-submit
{
   background-color:#cccccc !important;
   color: #ffffff;
   border-style: none !important;
   
}
.button-ok
{
   background-color:#aaaaaa !important;
   color: #ffffff !important;
   border-style: none !important;
   
   
}
.button-cancel
{
   background-color:#bbbbbb !important;
   color: #ffffff !important;
   border-style: none !important;
   
}
.detail-block
{
    background-color: #ffffff;  
    border-radius: 10px;
}
 .background-table
    {
         background-color: #ffffff;  
          border-radius: 10px;
    }
.empty-block
{
  height: 10px;
}
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #4e4e4ebd;
}
 .column-ym
    {
        width: 10px;
        max-width: 10px;
    }
</style>

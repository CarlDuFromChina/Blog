<template>
  <div></div>
</template>

<script>
export default {
  name: 'short-url',
  data() {
    return {
      shortid: this.$route.params.shortid || ''
    };
  },
  async created() {
    this.data = await sp.get(`api/ShortUrl/GetDataByShortId?id=${this.shortid}`);
    window.location.href = this.data.long_url;
  }
};
</script>

<style></style>
